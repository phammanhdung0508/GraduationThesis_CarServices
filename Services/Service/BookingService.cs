using System.Diagnostics;
using System.Globalization;
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
                .Map<Booking?, BookingDetailResponseDto>(await bookingRepository.Detail(id),
                otp => otp.AfterMap((src, des) =>
                {
                    des.BookingStatus = src!.BookingStatus.ToString();
                    des.PaymentStatus = src.PaymentStatus.ToString();
                    des.BookingStatus = src.BookingStatus.ToString();
                }));

                return booking;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<BookingPerHour>> IsBookingAvailable(BookingCheckRequestDto requestDto)
        {
            try
            {
                var garage = await garageRepository.GetGarage(requestDto.GarageId);
                var dateSelect = DateTime.Parse(requestDto.DateSelected);
                var openAt = DateTime.ParseExact(garage!.OpenAt, "hh:mm tt", CultureInfo.InvariantCulture).TimeOfDay.Hours;
                var closeAt = DateTime.ParseExact(garage!.CloseAt, "hh:mm tt", CultureInfo.InvariantCulture).TimeOfDay.Hours;

                var watch = System.Diagnostics.Stopwatch.StartNew();
                var listHours = new List<BookingPerHour>();

                await Task.Run(() =>
                {
                    for (int i = openAt; i <= closeAt; i++)
                    {
                        var time = new TimeSpan(i, 00, 00);
                        listHours.Add(new BookingPerHour { Hour = dateSelect.Add(time).ToString("h:mm:ss tt"), IsAvailable = true });
                    }
                });

                var listBooking = await bookingRepository.FilterBookingByDate(dateSelect, requestDto.GarageId);
                var lotCount = garage.Lots.Count;

                await Task.Run(() =>
                {
                    for (int i = openAt; i <= closeAt; i++)
                    {
                        var bookingCount = listBooking?
                        .Where(b => b.BookingTime.TimeOfDay.Hours.Equals(i)).Count();

                        //Check if is enough Booking per Hour

                        if (bookingCount.Equals(lotCount))
                        {
                            var selectedHour = i;
                            var minEstimatedTime = GetMinEstimatedTime(i, listBooking!);

                            //If all Booking have estimated time all > 2 skip to the next available Hour
                            for (int y = selectedHour; y <= selectedHour + minEstimatedTime - 1; y++)
                            {
                                UpdateListHours(y, listHours);
                            }
                            // Parallel.For(selectedHour, selectedHour + minEstimatedTime - 1, y =>{
                            //     UpdateListHours(y, listHours);
                            // });

                            i = selectedHour + minEstimatedTime - 1;
                        }

                        //Check if between two Hour if they all enough Booking with condition Hour one have Booking with estimated time == 1

                        var bookingInOneHours = listBooking?
                            .Where(b => b.BookingTime.TimeOfDay.Hours.Equals(i) && b.TotalEstimatedCompletionTime == 1).Count();

                        if (bookingCount.Equals(lotCount) && bookingInOneHours > 0)
                        {
                            (int? bookingInFirstHourCount, int? bookingInNextHourCount) = CountBookingPerHour(1, i, listBooking!);

                            if (bookingInFirstHourCount + bookingInNextHourCount == lotCount)
                            {
                                UpdateListHours(i + 1, listHours);
                            }
                        }

                        //Check if between two Hour if they all enough Booking with condition Hour

                        if (bookingCount > 0 && !bookingCount.Equals(lotCount))
                        {
                            var lastHour = closeAt;
                            var remainHour = lastHour - i;

                            var minEstimatedTimePerHour = GetMinEstimatedTime(i, listBooking!);

                            for (int z = 1; z <= remainHour; z++)
                            {
                                (int? bookingInFirstHourCount, int? bookingInNextHourCount) = CountBookingPerHour(z, i, listBooking!);

                                if (bookingInFirstHourCount + bookingInNextHourCount == lotCount && minEstimatedTimePerHour > 1)
                                {
                                    UpdateListHours(i + z, listHours);
                                }
                            }
                        }
                    }
                });

                watch.Stop();
                Debug.WriteLine($"\nTotal run time (Milliseconds): {watch.ElapsedMilliseconds}\n");

                return listHours;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private int GetMinEstimatedTime(int i, List<Booking> listBooking)
        {
            var minEstimatedTimePerHour = listBooking!.Where(b => b.BookingTime.TimeOfDay.Hours.Equals(i))
            .Min(l => l.TotalEstimatedCompletionTime);

            return minEstimatedTimePerHour;
        }

        private (int? bookingInFirstHourCount, int? bookingInNextHourCount) CountBookingPerHour(int num, int i, List<Booking> listBooking)
        {
            var bookingInFirstHourCount = listBooking?
            .Where(b => b.BookingTime.TimeOfDay.Hours.Equals(i) && b.TotalEstimatedCompletionTime > 1).Count();
            var bookingInNextHourCount = listBooking?
            .Where(b => b.BookingTime.TimeOfDay.Hours.Equals(i + num)).Count();

            return (bookingInFirstHourCount, bookingInNextHourCount);
        }

        private void UpdateListHours(int num, List<BookingPerHour> listHours)
        {
            listHours.FirstOrDefault(l => DateTime.Parse(l.Hour).TimeOfDay.Hours.Equals(num))!.IsAvailable = false;
        }

        public async Task<bool> Create(BookingCreateRequestDto requestDto)
        {
            try
            {
                var lotList = await lotRepository.GetAllLotInGarage(requestDto.GarageId);
                if (lotList!.Any(l => l.LotStatus.Equals(LotStatus.Free)))
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
                                await Run(requestDto);
                            }
                            break;
                        case false:
                            await Run(requestDto);
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

        private async Task Run(BookingCreateRequestDto requestDto)
        {
            try
            {
                var watch = System.Diagnostics.Stopwatch.StartNew();

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
                int totalEstimated = 0;

                var listService = mapper.Map<List<ServiceListDto>, List<ServiceBooking>>(requestDto.ServiceList,
                otp => otp.AfterMap((src, des) =>
                {
                    for (int i = 0; i < requestDto.ServiceList.Count; i++)
                    {
                        if (requestDto.ServiceList[i].ProductId == 0)
                        {
                            des[i].ProductId = null;
                        }
                        float productCost = 0, serviceCost = 0;
                        int serviceDuration = 0;
                        if (requestDto.ServiceList[i].ProductId > 0)
                        {
                            productCost = productRepository.GetPrice(src[i].ProductId);
                        }
                        if (requestDto.ServiceList[i].ServiceId > 0)
                        {
                            (serviceCost, serviceDuration) = serviceRepository.GetPriceAndDuration(src[i].ServiceId);
                        }
                        totalEstimated += serviceDuration;
                        totalPrice += productCost + serviceCost;
                        des[i].BookingId = bookingId;
                        des[i].ProductCost = productCost;
                        des[i].ServiceCost = serviceCost;
                    }
                }));
                await serviceBookingRepository.Create(listService);

                booking.TotalEstimatedCompletionTime = totalEstimated;
                booking.TotalPrice = totalPrice;
                await bookingRepository.Update(booking);

                watch.Stop();
                Debug.WriteLine($"Total run time: {watch.ElapsedMilliseconds}");
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