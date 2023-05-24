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
        private readonly ICouponRepository couponRepository;
        private readonly IServiceRepository serviceRepository;
        private readonly IGarageRepository garageRepository;
        private readonly ILotRepository lotRepository;
        private readonly ICarRepository carRepository;
        private readonly IMapper mapper;
        public BookingService(IBookingRepository bookingRepository, ILotRepository lotRepository,
        IMapper mapper, IServiceBookingRepository serviceBookingRepository, IProductRepository productRepository,
        IServiceRepository serviceRepository, IGarageRepository garageRepository, ICarRepository carRepository,
        ICouponRepository couponRepository)
        {
            this.mapper = mapper;
            this.bookingRepository = bookingRepository;
            this.serviceBookingRepository = serviceBookingRepository;
            this.lotRepository = lotRepository;
            this.productRepository = productRepository;
            this.serviceRepository = serviceRepository;
            this.garageRepository = garageRepository;
            this.carRepository = carRepository;
            this.couponRepository = couponRepository;
        }

        public async Task<List<BookingListResponseDto>?> View(PageDto page)
        {
            try
            {
                var list = mapper.Map<List<BookingListResponseDto>>(await bookingRepository.View(page));

                return list;
            }
            catch (Exception e)
            {
                var inner = e.InnerException;
                while (inner != null)
                {
                    Console.WriteLine(inner.StackTrace);
                    inner = inner.InnerException;
                }
                Debug.WriteLine(e.Message + "\r\n" + e.StackTrace + "\r\n" + inner);
                throw;
            }
        }

        public async Task<List<BookingListResponseDto>?> FilterBookingByGarageId(PagingBookingPerGarageRequestDto requestDto)
        {
            try
            {
                var isGarageExist = await garageRepository.IsGarageExist(requestDto.GarageId);

                switch (false)
                {
                    case var isExist when isExist == isGarageExist:
                        throw new NullReferenceException("The garage doesn't exist.");
                }

                var page = new PageDto
                {
                    PageIndex = requestDto.PageIndex,
                    PageSize = requestDto.PageSize
                };

                var list = mapper.Map<List<BookingListResponseDto>>(await bookingRepository.FilterBookingByGarageId(requestDto.GarageId, page));

                return list;
            }
            catch (Exception e)
            {
                var inner = e.InnerException;
                while (inner != null)
                {
                    Console.WriteLine(inner.StackTrace);
                    inner = inner.InnerException;
                }
                Debug.WriteLine(e.Message + "\r\n" + e.StackTrace + "\r\n" + inner);
                throw;
            }
        }

        public async Task<BookingDetailResponseDto?> Detail(int id)
        {
            try
            {
                var isBookingExist = await bookingRepository.IsBookingExist(id);

                switch (false)
                {
                    case var isExist when isExist == isBookingExist:
                        throw new NullReferenceException("The booking doesn't exist.");
                }

                var booking = mapper.Map<Booking?, BookingDetailResponseDto>(await bookingRepository.Detail(id),
                otp => otp.AfterMap((src, des) =>
                {
                    des.BookingStatus = src!.BookingStatus.ToString();
                    des.PaymentStatus = src.PaymentStatus.ToString();
                    des.BookingStatus = src.BookingStatus.ToString();
                }));

                return booking;
            }
            catch (Exception e)
            {
                var inner = e.InnerException;
                while (inner != null)
                {
                    Console.WriteLine(inner.StackTrace);
                    inner = inner.InnerException;
                }
                Debug.WriteLine(e.Message + "\r\n" + e.StackTrace + "\r\n" + inner);
                throw;
            }
        }

        public async Task<List<BookingPerHour>> IsBookingAvailable(BookingCheckRequestDto requestDto)
        {
            try
            {
                var garage = await garageRepository.GetGarage(requestDto.GarageId);
                var currentDay = DateTime.Now;
                var dateSelect = DateTime.Parse(requestDto.DateSelected);

                switch (false)
                {
                    case var isExist when isExist == (garage != null):
                        throw new NullReferenceException("The garage doesn't exist.");
                    case var isDate when isDate == (dateSelect.Date >= currentDay.Date):
                        throw new ArgumentOutOfRangeException("The selected date must be greater than or equal to the current date.");
                }

                var openAt = DateTime.ParseExact(garage!.OpenAt, "hh:mm tt", CultureInfo.InvariantCulture).TimeOfDay.Hours;
                var closeAt = DateTime.ParseExact(garage!.CloseAt, "hh:mm tt", CultureInfo.InvariantCulture).TimeOfDay.Hours;

                var watch = System.Diagnostics.Stopwatch.StartNew();
                var listHours = new List<BookingPerHour>();

                await Task.Run(() =>
                {
                    CreateListHourPerDay(openAt, closeAt, listHours, dateSelect);
                });

                var listBooking = await bookingRepository.FilterBookingByDate(dateSelect, requestDto.GarageId);
                var lotCount = garage.Lots.Count;

                await Task.Run(() =>
                {
                    CheckIfGarageAvailablePerHour(openAt, closeAt, listBooking!, lotCount, listHours);
                });

                var isAvailableList = listHours.Where(l => l.IsAvailable.Equals(true)).AsParallel()
                .Select(l => DateTime.Parse(l.Hour).TimeOfDay.Hours).Order().ToList();
                var sequenceLength = 1;

                await Task.Run(() =>
                {
                    UpdateEstimatedTimeCanBeBook(sequenceLength, isAvailableList, listHours);
                });

                watch.Stop();
                Debug.WriteLine($"\nTotal run time (Milliseconds): {watch.ElapsedMilliseconds}\n");

                return listHours;
            }
            catch (Exception e)
            {
                var inner = e.InnerException;
                while (inner != null)
                {
                    Console.WriteLine(inner.StackTrace);
                    inner = inner.InnerException;
                }
                Debug.WriteLine(e.Message + "\r\n" + e.StackTrace + "\r\n" + inner);
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

        private void EstimatedTimeCanBeBook(int from, int to, int estimatedTime, int sequenceLength, List<int> isAvailableList, List<BookingPerHour> listHours)
        {
            if (sequenceLength > 1)
            {
                for (int i = from; i <= to; i++)
                {
                    listHours.AsParallel().FirstOrDefault(l => DateTime.Parse(l.Hour).TimeOfDay.Hours
                    .Equals(i))!.EstimatedTimeCanBeBook = estimatedTime - i;
                }
            }
        }

        private void CreateListHourPerDay(int openAt, int closeAt, List<BookingPerHour> listHours, DateTime dateSelect)
        {
            for (int i = openAt; i <= closeAt; i++)
            {
                var time = new TimeSpan(i, 00, 00);
                listHours.Add(new BookingPerHour { Hour = dateSelect.Add(time).ToString("h:mm:ss tt") });
            }
        }

        private void UpdateEstimatedTimeCanBeBook(int sequenceLength, List<int> isAvailableList, List<BookingPerHour> listHours)
        {
            for (int i = 0; i <= isAvailableList.Count - 1; i++)
            {
                var currentHour = isAvailableList[i];

                if (isAvailableList[i + 1] - isAvailableList[i] == 1)
                {
                    sequenceLength++;
                }
                else
                {
                    listHours.FirstOrDefault(l => DateTime.Parse(l.Hour).TimeOfDay.Hours
                        .Equals(isAvailableList[i]))!.EstimatedTimeCanBeBook = 1;

                    EstimatedTimeCanBeBook(isAvailableList[i] - sequenceLength + 1, isAvailableList[i] + 1, isAvailableList[i] + 1, sequenceLength, isAvailableList, listHours);

                    sequenceLength = 1;
                }

                var isLastNumber = isAvailableList[i] + 1;

                if (isLastNumber.Equals(isAvailableList.Last()))
                {
                    EstimatedTimeCanBeBook(isAvailableList[i] - sequenceLength + 2, isAvailableList[i] + 1, isAvailableList[i] + 2, sequenceLength, isAvailableList, listHours);
                    break;
                }
            }
        }

        private void CheckIfGarageAvailablePerHour(int openAt, int closeAt, List<Booking> listBooking, int lotCount, List<BookingPerHour> listHours)
        {
            for (int i = openAt; i <= closeAt; i++)
            {
                var bookingCount = listBooking?
                .Where(b => b.BookingTime.TimeOfDay.Hours.Equals(i)).Count();

                var bookingInOneHours = listBooking?
                .Where(b => b.BookingTime.TimeOfDay.Hours.Equals(i) && b.TotalEstimatedCompletionTime == 1).Count();

                switch (bookingCount)
                {
                    case var bookingCout when bookingCout == lotCount:
                        var selectedHour = i;
                        var minEstimatedTime = GetMinEstimatedTime(i, listBooking!);

                        //If all Booking have estimated time all > 2 skip to the next available Hour
                        for (int y = selectedHour; y <= selectedHour + minEstimatedTime - 1; y++)
                        {
                            UpdateListHours(y, listHours);
                        }

                        i = selectedHour + minEstimatedTime - 1;
                        break;
                    case var bookingCout when bookingCout == lotCount && bookingInOneHours > 0:
                        (int? bookingInFirstHourCount, int? bookingInNextHourCount) = CountBookingPerHour(1, i, listBooking!);

                        if (bookingInFirstHourCount + bookingInNextHourCount == lotCount)
                        {
                            UpdateListHours(i + 1, listHours);
                        }
                        break;
                    case var bookingCout when bookingCout != lotCount && bookingCount > 0:
                        var lastHour = closeAt;
                        var remainHour = lastHour - i;

                        var minEstimatedTimePerHour = GetMinEstimatedTime(i, listBooking!);

                        for (int z = 1; z <= remainHour; z++)
                        {
                            (int? bookingInFirstHourCout, int? bookingInNextHourCout) = CountBookingPerHour(z, i, listBooking!);

                            if (bookingInFirstHourCout + bookingInNextHourCout == lotCount && minEstimatedTimePerHour > 1)
                            {
                                UpdateListHours(i + z, listHours);
                            }
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        public async Task Create(BookingCreateRequestDto requestDto)
        {
            try
            {
                var watch = System.Diagnostics.Stopwatch.StartNew();

                var bookingTime = DateOnly.Parse(requestDto.DateSelected).ToDateTime(TimeOnly.Parse(requestDto.TimeSelected));
                var currentDay = DateTime.Now;

                var garage = await garageRepository.GetGarage(requestDto.GarageId);
                var IsCarExist = await carRepository.IsCarExist(requestDto.CarId);

                if (requestDto.CouponId > 0)
                {
                    if (!await couponRepository.IsCouponExist(requestDto.CouponId))
                    {
                        throw new NullReferenceException("The coupon doesn't exist.");
                    }
                }

                var totalEstimated = 0;

                for (int i = 0; i < requestDto.ServiceList.Count; i++)
                {
                    var serviceDuration = await serviceRepository.GetDuration(requestDto.ServiceList[i].ServiceId);
                    totalEstimated += serviceDuration;
                }

                var bookingCheck = new BookingCheckRequestDto { DateSelected = requestDto.DateSelected, GarageId = requestDto.GarageId };
                var listHours = await IsBookingAvailable(bookingCheck);
                var isAvailableHours = listHours.FirstOrDefault(l => DateTime.Parse(l.Hour).TimeOfDay.Hours == bookingTime.TimeOfDay.Hours);

                switch (false)
                {
                    case var isExist when isExist == IsCarExist:
                        throw new NullReferenceException("The car doesn't exist.");
                    case var isTime when isTime == (bookingTime >= currentDay):
                        throw new ArgumentOutOfRangeException("The selected time must be greater than or equal to the current time.");
                    case var isEmpty when isEmpty == (requestDto.ServiceList.Count > 0):
                        throw new ArgumentException("Must select the service before booking.");
                    case var isFalse when isFalse == (isAvailableHours!.IsAvailable == true && isAvailableHours.EstimatedTimeCanBeBook > totalEstimated):
                        throw new ArgumentException("Estimated hours for service will conflict with another booking.");
                }

                var listBooking = await bookingRepository.FilterBookingByTimePerDay(bookingTime, requestDto.GarageId);
                var lotCount = garage!.Lots.Count;

                if (lotCount - listBooking!.Count == 1)
                {
                    Debug.WriteLine($"{System.Text.Encoding.Default.GetString(garage!.VersionNumber)}");
                    if (requestDto.VersionNumber.SequenceEqual(garage!.VersionNumber))
                    {
                        await garageRepository.Update(garage);
                        await Run(requestDto, bookingTime, totalEstimated);
                    }
                    else
                    {
                        throw new TaskCanceledException("Sorry, there is someone before you booked this.");
                    }
                }
                else
                {
                    await Run(requestDto, bookingTime, totalEstimated);
                }

                watch.Stop();
                Debug.WriteLine($"\nTotal run time (Milliseconds) Create(): {watch.ElapsedMilliseconds}\n");
            }
            catch (Exception e)
            {
                var inner = e.InnerException;
                while (inner != null)
                {
                    Console.WriteLine(inner.StackTrace);
                    inner = inner.InnerException;
                }
                Debug.WriteLine(e.Message + "\r\n" + e.StackTrace + "\r\n" + inner);
                throw;
            }
        }

        private async Task Run(BookingCreateRequestDto requestDto, DateTime bookingTime, int totalEstimated)
        {
            try
            {
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

                float discountedPrice = 0;
                float originalPrice = 0;

                var listService = mapper.Map<List<ServiceListDto>, List<ServiceBooking>>(requestDto.ServiceList,
                otp => otp.AfterMap((src, des) =>
                {
                    for (int i = 0; i < requestDto.ServiceList.Count; i++)
                    {
                        float productCost = 0, serviceCost = 0;
                        if (requestDto.ServiceList[i].ProductId == 0)
                        {
                            des[i].ProductId = null;
                        }
                        if (requestDto.ServiceList[i].ProductId > 0)
                        {
                            productCost = productRepository.GetPrice(src[i].ProductId);
                        }
                        if (requestDto.ServiceList[i].ServiceId > 0)
                        {
                            serviceCost = serviceRepository.GetPrice(src[i].ServiceId);
                        }
                        originalPrice += productCost + serviceCost;
                        des[i].BookingId = bookingId;
                        des[i].ProductCost = productCost;
                        des[i].ServiceCost = serviceCost;
                    }
                }));

                await serviceBookingRepository.Create(listService);

                if (requestDto.CouponId > 0)
                {
                    var coupon = await couponRepository.Detail(requestDto.CouponId);

                    switch (coupon!.CouponType)
                    {
                        case CouponType.Percent:
                            discountedPrice = originalPrice * (coupon.CouponValue / 100);
                            break;
                        case CouponType.FixedAmount:
                            discountedPrice = originalPrice - coupon.CouponValue;
                            break;
                    }
                }

                if (discountedPrice > 0)
                {
                    booking.TotalPrice = discountedPrice;
                }
                else
                {
                    booking.TotalPrice = originalPrice;
                }

                booking.TotalEstimatedCompletionTime = totalEstimated;

                await bookingRepository.Update(booking);
            }
            catch (Exception e)
            {
                var inner = e.InnerException;
                while (inner != null)
                {
                    Console.WriteLine(inner.StackTrace);
                    inner = inner.InnerException;
                }
                Debug.WriteLine(e.Message + "\r\n" + e.StackTrace + "\r\n" + inner);
                throw;
            }
        }

        public async Task UpdateStatus(BookingStatusRequestDto requestDto)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();

            var b = await bookingRepository.Detail(requestDto.BookingId);

            switch (false)
            {
                case var isExist when isExist == (b != null):
                    throw new NullReferenceException("The booking doesn't exist.");
            }

            var booking = mapper.Map<BookingStatusRequestDto, Booking>(requestDto, b!);
            await bookingRepository.Update(booking);

            switch (requestDto.BookingStatus)
            {
                case BookingStatus.CheckIn:
                    await UpdateLotStatus(LotStatus.Assigned, booking);
                    break;
                case BookingStatus.Processing:
                    await UpdateLotStatus(LotStatus.BeingUsed, booking);
                    break;
                case BookingStatus.Completed:
                    await UpdateLotStatus(LotStatus.Free, booking);
                    break;
            }

            watch.Stop();
            Debug.WriteLine($"Total run time (Milliseconds) Run(): {watch.ElapsedMilliseconds}");
        }

        private async Task UpdateLotStatus(LotStatus status, Booking booking)
        {
            if (status == LotStatus.Assigned)
            {
                var lot = await lotRepository.GetFreeLotInGarage((int)booking.GarageId!);
                var licensePlate = await carRepository.GetLicensePlate((int)booking.CarId!);

                lot.LotStatus = status;
                lot.IsAssignedFor = licensePlate;

                await lotRepository.Update(lot);
            }
            else
            {
                var lot = await lotRepository.GetLotByLicensePlate((int)booking.GarageId!, booking.Car.CarLicensePlate);

                lot.LotStatus = status;
                if (status == LotStatus.Free)
                {
                    lot.IsAssignedFor = null;
                }

                await lotRepository.Update(lot);
            }
        }
    }
}