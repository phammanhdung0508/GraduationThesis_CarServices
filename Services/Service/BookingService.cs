using System.Diagnostics;
using AutoMapper;
using GraduationThesis_CarServices.Enum;
using GraduationThesis_CarServices.Models.DTO.Booking;
using GraduationThesis_CarServices.Models.DTO.Page;
using GraduationThesis_CarServices.Models.Entity;
using GraduationThesis_CarServices.Repositories.IRepository;
using GraduationThesis_CarServices.Services.IService;

namespace GraduationThesis_CarServices.Services.Service
{
    public class BookingService : IBookingService
    {
        private readonly IServiceBookingRepository serviceBookingRepository;
        private readonly IBookingRepository bookingRepository;
        private readonly IProductRepository productRepository;
        private readonly IServiceRepository serviceRepository;
        private readonly IGarageRepository garageRepository;
        private readonly ILotRepository lotRepository;
        private readonly IMapper mapper;
        public BookingService(IBookingRepository bookingRepository, ILotRepository lotRepository,
        IMapper mapper, IServiceBookingRepository serviceBookingRepository, IProductRepository productRepository,
        IServiceRepository serviceRepository, IGarageRepository garageRepository)
        {
            this.mapper = mapper;
            this.bookingRepository = bookingRepository;
            this.serviceBookingRepository = serviceBookingRepository;
            this.lotRepository = lotRepository;
            this.productRepository = productRepository;
            this.serviceRepository = serviceRepository;
            this.garageRepository = garageRepository;
        }

        public async Task<List<BookingListResponseDto>?> View(PageDto page)
        {
            try
            {
                var list = mapper
                .Map<List<BookingListResponseDto>>(await bookingRepository.View(page));

                return list;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<BookingDetailResponseDto?> Detail(int id)
        {
            try
            {
                var booking = mapper
                .Map<BookingDetailResponseDto>(await bookingRepository.Detail(id));

                return booking;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> Create(BookingCreateRequestDto requestDto)
        {
            try
            {
                var lotList = await lotRepository.GetAllLotInGarage(requestDto.GarageId);
                if (lotList!.All(l => l.LotStatus.Equals(LotStatus.Free)))
                {
                    var lotFree = lotList!.Where(l => l.LotStatus == LotStatus.Free);
                    var lot = lotFree!.OrderBy(l => l.LotStatus).FirstOrDefault();
                    switch (lotFree.Count() == 1)
                    {
                        case true:
                            var garage = await garageRepository.GetGarage(requestDto.GarageId);
                            Debug.WriteLine($"{System.Text.Encoding.Default.GetString(garage!.VersionNumber)}");
                            if (requestDto.VersionNumber.SequenceEqual(garage!.VersionNumber))
                            {
                                await garageRepository.Update(garage);
                                await Run(requestDto, lot!);
                            }
                            break;
                        case false:
                            await Run(requestDto, lot!);
                            break;
                        default:
                    }
                }
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private async Task Run(BookingCreateRequestDto requestDto, Lot lot)
        {
            try
            {
                DateTime bookingTime = DateOnly.Parse(requestDto.DateSelected).ToDateTime(TimeOnly.Parse(requestDto.TimeSelected));
                var booking = mapper.Map<BookingCreateRequestDto, Booking>(requestDto,
                otp => otp.AfterMap((src, des) =>
                {
                    var now = DateTime.Now;
                    des.BookingTime = bookingTime;
                    des.CreatedAt = now;
                    switch (now.Date)
                    {
                        case var nowDate when nowDate < bookingTime.Date:
                            des.BookingStatus = BookingStatus.NotStart;
                            break;
                        case var nowDate when nowDate == bookingTime.Date:
                            des.BookingStatus = BookingStatus.AppointmentDay;
                            break;
                    }
                }));
                var bookingId = await bookingRepository.Create(booking);

                // lot!.LotStatus = LotStatus.AlreadyBooked;
                // await lotRepository.Update(lot);

                float totalPrice = 0;

                var listService = mapper.Map<List<ServiceListDto>, List<ServiceBooking>>(requestDto.ServiceList,
                otp => otp.AfterMap((src, des) =>
                {
                    for (int i = 0; i < requestDto.ServiceList.Count; i++)
                    {
                        float productCost = 0, serviceCost = 0;
                        if (requestDto.ServiceList[i].ProductId > 0)
                        {
                            productCost = productRepository.GetPrice(src[i].ProductId);
                        }
                        if (requestDto.ServiceList[i].ServiceId > 0)
                        {
                            serviceCost = serviceRepository.GetPrice(src[i].ServiceId);
                        }
                        totalPrice += productCost + serviceCost;
                        des[i].BookingId = bookingId;
                        des[i].ProductCost = productCost;
                        des[i].ServiceCost = serviceCost;
                    }
                }));
                await serviceBookingRepository.Create(listService);

                booking.TotalPrice = totalPrice;
                await bookingRepository.Update(booking);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> Update(BookingUpdateRequestDto requestDto)
        {
            try
            {
                var b = await bookingRepository.Detail(requestDto.BookingId);
                var booking = mapper.Map<BookingUpdateRequestDto, Booking>(requestDto, b!,
                otp => otp.AfterMap((src, des) =>
                {
                    des.UpdatedAt = DateTime.Now;
                }));
                await bookingRepository.Update(booking);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}