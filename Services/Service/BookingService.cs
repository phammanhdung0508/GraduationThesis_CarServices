using System.Diagnostics;
using System.Drawing.Imaging;
using System.Globalization;
using AutoMapper;
using GraduationThesis_CarServices.Enum;
using GraduationThesis_CarServices.Models.DTO.Booking;
using GraduationThesis_CarServices.Models.DTO.Exception;
using GraduationThesis_CarServices.Models.DTO.Page;
using GraduationThesis_CarServices.Models.Entity;
using GraduationThesis_CarServices.Repositories.IRepository;
using GraduationThesis_CarServices.Services.IService;
using QRCoder;

namespace GraduationThesis_CarServices.Services.Service
{
    public class BookingService : IBookingService
    {
        private readonly IBookingDetailRepository bookingDetailRepository;
        private readonly IBookingRepository bookingRepository;
        private readonly IProductRepository productRepository;
        private readonly ICouponRepository couponRepository;
        private readonly IServiceRepository serviceRepository;
        private readonly IGarageRepository garageRepository;
        private readonly IMechanicRepository mechanicRepository;
        private readonly ILotRepository lotRepository;
        private readonly ICarRepository carRepository;
        private readonly HttpClient httpClient;
        private readonly IMapper mapper;
        public BookingService(IBookingRepository bookingRepository, ILotRepository lotRepository,
        IMapper mapper, IBookingDetailRepository bookingDetailRepository, IProductRepository productRepository,
        IServiceRepository serviceRepository, IGarageRepository garageRepository, ICarRepository carRepository,
        ICouponRepository couponRepository, IMechanicRepository mechanicRepository)
        {
            this.mapper = mapper;
            this.httpClient = new HttpClient();
            this.bookingRepository = bookingRepository;
            this.bookingDetailRepository = bookingDetailRepository;
            this.lotRepository = lotRepository;
            this.productRepository = productRepository;
            this.serviceRepository = serviceRepository;
            this.garageRepository = garageRepository;
            this.carRepository = carRepository;
            this.couponRepository = couponRepository;
            this.mechanicRepository = mechanicRepository;
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
                switch (e)
                {
                    case MyException:
                        throw;
                    default:
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
        }

        public async Task<List<BookingListResponseDto>?> FilterBookingByGarageId(PagingBookingPerGarageRequestDto requestDto)
        {
            try
            {
                var isGarageExist = await garageRepository.IsGarageExist(requestDto.GarageId);

                switch (false)
                {
                    case var isExist when isExist == isGarageExist:
                        throw new MyException("The garage doesn't exist.", 404);
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
                switch (e)
                {
                    case MyException:
                        throw;
                    default:
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
        }

        public async Task<List<FilterByCustomerResponseDto>?> FilterBoookingByCustomer(FilterByCustomerRequestDto requestDto)
        {
            try
            {
                var page = new PageDto
                {
                    PageIndex = requestDto.PageIndex,
                    PageSize = requestDto.PageSize
                };

                var list = mapper.Map<List<FilterByCustomerResponseDto>>(await bookingRepository.FilterBookingByCustomer(requestDto.UserId, page));

                return list;
            }
            catch (Exception e)
            {
                switch (e)
                {
                    case MyException:
                        throw;
                    default:
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
        }

        public async Task<BookingDetailResponseDto?> Detail(int id)
        {
            try
            {
                var isBookingExist = await bookingRepository.IsBookingExist(id);

                switch (false)
                {
                    case var isExist when isExist == isBookingExist:
                        throw new MyException("The booking doesn't exist.", 404);
                }

                var booking = mapper.Map<Booking?, BookingDetailResponseDto>(await bookingRepository.Detail(id));

                return booking;
            }
            catch (Exception e)
            {
                switch (e)
                {
                    case MyException:
                        throw;
                    default:
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
                        throw new MyException("The garage doesn't exist.", 404);
                    case var isDate when isDate == (dateSelect.Date >= currentDay.Date):
                        throw new MyException("The selected date must be greater than or equal to the current date.", 404);
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
                    CheckIfGarageAvailablePerHour(openAt, closeAt, listBooking!, lotCount, listHours, dateSelect);
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
                switch (e)
                {
                    case MyException:
                        throw;
                    default:
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

                if (isAvailableList[i + 1].Equals(isAvailableList.Last()))
                {
                    EstimatedTimeCanBeBook(isAvailableList[i] - sequenceLength + 2, isAvailableList[i] + 1, isAvailableList[i] + 2, sequenceLength, isAvailableList, listHours);
                    break;
                }
            }
        }

        private void CheckIfGarageAvailablePerHour(int openAt, int closeAt, List<Booking> listBooking, int lotCount, List<BookingPerHour> listHours, DateTime dateSelect)
        {
            for (int i = openAt; i <= closeAt; i++)
            {
                var current = DateTime.Now;
                var convertHour = 0;
                var selectedHour = i;

                switch (current.Hour)
                {
                    case var hour when hour > 12:
                        convertHour = hour - 12;
                        break;
                    default:
                        convertHour = current.Hour;
                        break;
                }

                var bookingCount = listBooking?
                .Where(b => b.BookingTime.TimeOfDay.Hours.Equals(i)).Count();

                var bookingInOneHours = listBooking?
                .Where(b => b.BookingTime.TimeOfDay.Hours.Equals(i) && b.TotalEstimatedCompletionTime == 1).Count();

                switch (bookingCount)
                {
                    case var bookingCout when i - convertHour <= 3 && (current.Date == dateSelect.Date):
                        UpdateListHours(i, listHours);
                        break;
                    case var bookingCout when bookingCout == lotCount:
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
                        var remainHour = closeAt - i;

                        var minEstimatedTimePerHour = GetMinEstimatedTime(i, listBooking!);

                        for (int z = 1; z <= remainHour; z++)
                        {
                            (int? bookingInFirstHourCout, int? bookingInNextHourCout) = CountBookingPerHour(z, i, listBooking!);

                            if (bookingInFirstHourCout + bookingInNextHourCout == lotCount && minEstimatedTimePerHour > 1)
                            {
                                var durationFirstHour = listBooking?.Where(b => b.BookingTime.TimeOfDay.Hours.Equals(i) && b.TotalEstimatedCompletionTime > 1).FirstOrDefault()!.TotalEstimatedCompletionTime;
                                if (durationFirstHour > 1)
                                {
                                    for (int b = i + 1; b < i + durationFirstHour; b++)
                                    {
                                        UpdateListHours(b, listHours);
                                    }
                                    z = (int)durationFirstHour;
                                    i = i + z - 1;
                                    var isBookin = listBooking?.Where(b => b.BookingTime.TimeOfDay.Hours.Equals(i + 1) && b.TotalEstimatedCompletionTime > 1).Count();
                                    if (isBookin + (int)bookingInFirstHourCout! == lotCount)
                                    {
                                        UpdateListHours(i + 1, listHours);
                                    }
                                }
                                else
                                {
                                    UpdateListHours(i + z, listHours);
                                }
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
                        throw new MyException("The coupon doesn't exist.", 404);
                    }
                }

                var totalEstimated = 0;

                for (int i = 0; i < requestDto.ServiceList.Count; i++)
                {
                    var serviceDuration = await serviceRepository.GetDuration(requestDto.ServiceList[i].ServiceDetailId);
                    totalEstimated += serviceDuration;
                }

                var bookingCheck = new BookingCheckRequestDto { DateSelected = requestDto.DateSelected, GarageId = requestDto.GarageId };
                var listHours = await IsBookingAvailable(bookingCheck);
                var isAvailableHours = listHours.FirstOrDefault(l => DateTime.Parse(l.Hour).TimeOfDay.Hours == bookingTime.TimeOfDay.Hours);

                switch (false)
                {
                    case var isExist when isExist == IsCarExist:
                        throw new MyException("The car doesn't exist.", 404);
                    case var isTime when isTime == (bookingTime >= currentDay):
                        throw new MyException("The selected time must be greater than or equal to the current time.", 404);
                    case var isEmpty when isEmpty == (requestDto.ServiceList.Count > 0):
                        throw new MyException("Must select the service before booking.", 404);
                    case var isFalse when isFalse == (isAvailableHours!.IsAvailable == true && isAvailableHours.EstimatedTimeCanBeBook > totalEstimated):
                        throw new MyException("Estimated hours for service will conflict with another booking.", 404);
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
                        throw new MyException("Sorry, there is someone before you booked this.", 409);
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
                switch (e)
                {
                    case MyException:
                        throw;
                    default:
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

                var listService = mapper.Map<List<ServiceListDto>, List<BookingDetail>>(requestDto.ServiceList,
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
                        if (requestDto.ServiceList[i].ServiceDetailId > 0)
                        {
                            serviceCost = serviceRepository.GetPrice(src[i].ServiceDetailId);
                        }
                        originalPrice += productCost + serviceCost;
                        des[i].BookingId = bookingId;
                        des[i].ProductCost = productCost;
                        des[i].ServiceCost = serviceCost;
                    }
                }));

                await bookingDetailRepository.Create(listService);

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
                switch (e)
                {
                    case MyException:
                        throw;
                    default:
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
        }

        public async Task UpdateStatus(int bookingId, BookingStatus bookingStatus)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();

            var booking = await bookingRepository.Detail(bookingId);

            switch (false)
            {
                case var isExist when isExist == (booking != null):
                    throw new MyException("The booking doesn't exist.", 404);
            }

            booking!.BookingStatus = bookingStatus;

            //await bookingRepository.Update(booking);

            // switch (bookingStatus)
            // {
            //     case BookingStatus.CheckIn:
            //         await UpdateLotStatus(LotStatus.Assigned, booking);
            //         break;
            //     case BookingStatus.Processing:
            //         await UpdateLotStatus(LotStatus.BeingUsed, booking);
            //         break;
            //     case BookingStatus.Completed:
            //         await UpdateLotStatus(LotStatus.Free, booking);
            //         break;
            // }

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

                //await lotRepository.Update(lot);

                await AssigneMechanicForBooking(booking);
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

        private async Task AssigneMechanicForBooking(Booking booking)
        {
            var bookingDetailList = await bookingDetailRepository.FilterServiceBookingByBookingId(booking.BookingId);
            var mechanicAvailableList = await mechanicRepository.FilterMechanicAvailableByGarageId((int)booking.GarageId!);

            switch (false)
            {
                case var isFalse when isFalse == (bookingDetailList.Count <= mechanicAvailableList.Count):
                    throw new MyException("There are not enough mechanic for booking.", 404);
            }

            var minWorkingHour = mechanicAvailableList.Take(bookingDetailList.Count).ToList();

            for (int i = 0; i < bookingDetailList.Count; i++)
            {
                bookingDetailList[i].MechanicId = minWorkingHour[i].MechanicId;
                var estimatedTime = await serviceRepository.GetDuration((int)bookingDetailList[i].ServiceDetail.ServiceId!);
                minWorkingHour[i].TotalWorkingHours += estimatedTime;
            }

            await bookingDetailRepository.Update(bookingDetailList);
        }

        public async Task<String> GenerateQRCode(int bookingId)
        {
            try
            {
                // Generate the QR code
                string url = $"https://localhost:7006/api/booking/run-qr-code/{bookingId}";

                var qrGenerator = new QRCodeGenerator();
                var qrCodeData = qrGenerator.CreateQrCode(url, QRCodeGenerator.ECCLevel.Q);
                var qrCode = new QRCode(qrCodeData);
                var qrCodeImage = qrCode.GetGraphic(10);

                string qrCodeImageBase64;
                await using (var ms = new MemoryStream())
                {
                    qrCodeImage.Save(ms, ImageFormat.Png);
                    byte[] imageBytes = ms.ToArray();
                    qrCodeImageBase64 = "data:image/png;base64," + Convert.ToBase64String(imageBytes);
                }

                string qrCodeImageUrl = qrCodeImageBase64;
                return qrCodeImageUrl;
            }
            catch (Exception e)
            {
                switch (e)
                {
                    case MyException:
                        throw;
                    default:
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
        }
    }
}