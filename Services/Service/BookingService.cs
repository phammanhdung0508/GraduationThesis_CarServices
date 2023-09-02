using System.Diagnostics;
using System.Globalization;
using System.Text;
using AutoMapper;
using Azure.Storage.Blobs;
using GraduationThesis_CarServices.Enum;
using GraduationThesis_CarServices.Mapping;
using GraduationThesis_CarServices.Models.DTO.Booking;
using GraduationThesis_CarServices.Models.DTO.Exception;
using GraduationThesis_CarServices.Models.DTO.Page;
using GraduationThesis_CarServices.Models.Entity;
using GraduationThesis_CarServices.Notification;
using GraduationThesis_CarServices.Paging;
using GraduationThesis_CarServices.PaymentGateway;
using GraduationThesis_CarServices.Repositories.IRepository;
using GraduationThesis_CarServices.Services.IService;
using QRCoder;

namespace GraduationThesis_CarServices.Services.Service
{
    public class BookingService : IBookingService
    {
        private readonly IVNPayPaymentGateway iVNPayPaymentGateway;
        private readonly IBookingDetailRepository bookingDetailRepository;
        private readonly IBookingRepository bookingRepository;
        private readonly IProductRepository productRepository;
        private readonly ICouponRepository couponRepository;
        private readonly IServiceRepository serviceRepository;
        private readonly IGarageRepository garageRepository;
        private readonly IMechanicRepository mechanicRepository;
        private readonly ILotRepository lotRepository;
        private readonly ICarRepository carRepository;
        private readonly IConfiguration configuration;
        private readonly FCMSendNotificationMobile fCMSendNotificationMobile;
        //private readonly HttpClient httpClient;
        private readonly IMapper mapper;
        public BookingService(IBookingRepository bookingRepository, ILotRepository lotRepository,
        IMapper mapper, IBookingDetailRepository bookingDetailRepository, IProductRepository productRepository,
        IServiceRepository serviceRepository, IGarageRepository garageRepository, ICarRepository carRepository,
        ICouponRepository couponRepository, IMechanicRepository mechanicRepository, IConfiguration configuration,
        IVNPayPaymentGateway iVNPayPaymentGateway, FCMSendNotificationMobile fCMSendNotificationMobile)
        {
            this.mapper = mapper;
            //httpClient = new HttpClient();
            this.bookingRepository = bookingRepository;
            this.bookingDetailRepository = bookingDetailRepository;
            this.lotRepository = lotRepository;
            this.productRepository = productRepository;
            this.serviceRepository = serviceRepository;
            this.garageRepository = garageRepository;
            this.carRepository = carRepository;
            this.couponRepository = couponRepository;
            this.mechanicRepository = mechanicRepository;
            this.configuration = configuration;
            this.iVNPayPaymentGateway = iVNPayPaymentGateway;
            this.fCMSendNotificationMobile = fCMSendNotificationMobile;
        }

        public async Task<List<BookingDetailStatusForBookingResponseDto>> GetBookingDetailStatusByBooking(int bookingId)
        {
            try
            {
                var list = await bookingDetailRepository.FilterBookingDetailByBookingId(bookingId);

                switch (false)
                {
                    case var isExist when isExist == (list is not null):
                        throw new MyException("The booking doesn't exist.", 404);
                }

                return mapper.Map<List<BookingDetailStatusForBookingResponseDto>>(list);
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

        public async Task<BookingServiceStatusForStaffResponseDto> GetBookingServiceStatusByBooking(int bookingId)
        {
            try
            {
                var booking = await bookingRepository.Detail(bookingId);

                foreach (var item in booking!.BookingMechanics)
                {
                    if (item.BookingMechanicStatus.Equals(Status.Deactivate))
                    {
                        booking.BookingMechanics.Remove(item);
                    }
                }

                var bookingDto = mapper.Map<BookingServiceStatusForStaffResponseDto>(booking);

                return bookingDto;
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

        public async Task<GenericObject<List<BookingListResponseDto>>> View(PageDto page)
        {
            try
            {
                (var listObj, var count) = await bookingRepository.View(page);

                var listDto = mapper.Map<List<BookingListResponseDto>>(listObj);

                var list = new GenericObject<List<BookingListResponseDto>>(listDto, count);

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

        public async Task<GenericObject<List<BookingListResponseDto>>> FilterBookingByStatus(FilterByStatusRequestDto requestDto)
        {
            try
            {
                var status = requestDto.BookingStatus;

                if (!typeof(BookingStatus).IsEnumDefined(status))
                {
                    throw new MyException("The status number is out of avaliable range.", 404);
                }

                var page = new PageDto { PageIndex = requestDto.PageIndex, PageSize = requestDto.PageSize };

                (var listObj, var count) = await bookingRepository.FilterBookingByStatus(status, page, requestDto.GarageId);

                var listDto = mapper.Map<List<BookingListResponseDto>>(listObj);

                var list = new GenericObject<List<BookingListResponseDto>>(listDto, count);

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

        public async Task<GenericObject<List<BookingListResponseDto>>> FilterBookingStatusAndDate(FilterByStatusAndDateRequestDto requestDto)
        {
            try
            {
                var status = requestDto.BookingStatus;
                DateTime? dateFrom = null;
                DateTime? dateTo = null;

                if (requestDto.DateFrom is not null)
                {
                    dateFrom = DateTime.Parse(requestDto.DateFrom!);
                }

                if (requestDto.DateTo is not null)
                {
                    dateTo = DateTime.Parse(requestDto.DateTo!);
                }

                if (status is not null && !typeof(BookingStatus).IsEnumDefined(status!))
                {
                    throw new MyException("The status number is out of avaliable range.", 404);
                }

                var page = new PageDto { PageIndex = requestDto.PageIndex, PageSize = requestDto.PageSize };

                (var listObj, var count) = await bookingRepository.FilterBookingStatusAndDate(dateFrom, dateTo, status, page);

                var listDto = mapper.Map<List<BookingListResponseDto>>(listObj);

                var list = new GenericObject<List<BookingListResponseDto>>(listDto, count);

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

        public async Task<GenericObject<List<BookingListResponseDto>>> SearchByBookingCode(SearchBookingByUserRequestDto requestDto)
        {
            try
            {
                var page = new PageDto { PageIndex = requestDto.PageIndex, PageSize = requestDto.PageSize };

                (var listObj, var count) = await bookingRepository.SearchByBookingCode(requestDto.UserId, requestDto.Search, page);

                var listDto = mapper.Map<List<BookingListResponseDto>>(listObj);

                var list = new GenericObject<List<BookingListResponseDto>>(listDto, count);

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

        public async Task<GenericObject<List<BookingListResponseDto>>> FilterBookingByGarageId(PagingBookingPerGarageRequestDto requestDto)
        {
            try
            {
                var isGarageExist = await garageRepository.IsGarageExist(requestDto.GarageId);

                switch (false)
                {
                    case var isExist when isExist == isGarageExist:
                        throw new MyException("The garage doesn't exist.", 404);
                }

                var page = new PageDto { PageIndex = requestDto.PageIndex, PageSize = requestDto.PageSize };

                (var listObj, var count) = await bookingRepository.FilterBookingByGarage(requestDto.GarageId, page);

                var listDto = mapper.Map<List<BookingListResponseDto>>(listObj);

                var list = new GenericObject<List<BookingListResponseDto>>(listDto, count);

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

        public async Task<GenericObject<List<FilterByCustomerResponseDto>>> FilterBoookingByCustomer(FilterByCustomerRequestDto requestDto)
        {
            try
            {
                var page = new PageDto
                {
                    PageIndex = requestDto.PageIndex,
                    PageSize = requestDto.PageSize
                };

                (var listObj, var count) = await bookingRepository.FilterBookingByCustomer(requestDto.UserId, page);

                var listDto = mapper.Map<List<FilterByCustomerResponseDto>>(listObj);

                var list = new GenericObject<List<FilterByCustomerResponseDto>>(listDto, count);

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
                    UpdateEstimatedTimeCanBeBook(sequenceLength, isAvailableList, listHours, requestDto.TotalEstimatedTimeServicesTake);
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

        private static int GetMinEstimatedTime(int i, List<Booking> listBooking)
        {
            var minEstimatedTimePerHour = listBooking!.Where(b => b.BookingTime.TimeOfDay.Hours.Equals(i))
            .Min(l => /*l.TotalEstimatedCompletionTime*/ l.CustomersCanReceiveTheCarTime);

            return minEstimatedTimePerHour;
        }

        private static (int? bookingInFirstHourCount, int? bookingInNextHourCount) CountBookingPerHour(int num, int i, List<Booking> listBooking)
        {
            var bookingInFirstHourCount = listBooking?
            .Where(b => b.BookingTime.TimeOfDay.Hours.Equals(i) && /*b.TotalEstimatedCompletionTime*/ b.CustomersCanReceiveTheCarTime > 1).Count();
            var bookingInNextHourCount = listBooking?
            .Where(b => b.BookingTime.TimeOfDay.Hours.Equals(i + num)).Count();

            return (bookingInFirstHourCount, bookingInNextHourCount);
        }

        private static void UpdateListHours(int num, List<BookingPerHour> listHours)
        {
            listHours.FirstOrDefault(l => DateTime.Parse(l.Hour).TimeOfDay.Hours.Equals(num))!.IsAvailable = false;
        }

        private static void EstimatedTimeCanBeBook(int from, int to, int estimatedTime, int sequenceLength, List<int> isAvailableList, List<BookingPerHour> listHours, int totalEstimatedTimeServicesTake)
        {
            if (sequenceLength > 1)
            {
                for (int i = from; i <= to; i++)
                {
                    // listHours.AsParallel().FirstOrDefault(l => DateTime.Parse(l.Hour).TimeOfDay.Hours
                    // .Equals(i))!.EstimatedTimeCanBeBook = estimatedTime - i;

                    if (totalEstimatedTimeServicesTake > estimatedTime - i)
                    {
                        UpdateListHours(i, listHours);
                    }
                }
            }
        }

        private static void CreateListHourPerDay(int openAt, int closeAt, List<BookingPerHour> listHours, DateTime dateSelect)
        {
            for (int i = openAt; i <= closeAt; i++)
            {
                var time = new TimeSpan(i, 00, 00);
                listHours.Add(new BookingPerHour { Hour = dateSelect.Add(time).ToString("HH:mm:ss") });
            }
        }

        private static void UpdateEstimatedTimeCanBeBook(int sequenceLength, List<int> isAvailableList, List<BookingPerHour> listHours, int totalEstimatedTimeServicesTake)
        {
            for (int i = 0; i <= isAvailableList.Count - 1; i++)
            {
                if (isAvailableList[i + 1] - isAvailableList[i] == 1)
                {
                    sequenceLength++;
                }
                else
                {
                    // listHours.FirstOrDefault(l => DateTime.Parse(l.Hour).TimeOfDay.Hours
                    //     .Equals(isAvailableList[i]))!.EstimatedTimeCanBeBook = 1;

                    if (totalEstimatedTimeServicesTake > 1)
                    {
                        UpdateListHours(isAvailableList[i], listHours);
                    }

                    EstimatedTimeCanBeBook(isAvailableList[i] - sequenceLength + 1, isAvailableList[i] + 1, isAvailableList[i] + 1, sequenceLength, isAvailableList, listHours, totalEstimatedTimeServicesTake);

                    sequenceLength = 1;
                }

                if (isAvailableList[i + 1].Equals(isAvailableList.Last()))
                {
                    EstimatedTimeCanBeBook(isAvailableList[i] - sequenceLength + 2, isAvailableList[i] + 1, isAvailableList[i] + 2, sequenceLength, isAvailableList, listHours, totalEstimatedTimeServicesTake);
                    break;
                }
            }
        }

        private static void CheckIfGarageAvailablePerHour(int openAt, int closeAt, List<Booking> listBooking, int lotCount, List<BookingPerHour> listHours, DateTime dateSelect)
        {
            for (int i = openAt; i <= closeAt; i++)
            {
                var timeZone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");
                var current = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, timeZone);

                var selectedHour = i;

                var bookingCount = listBooking?
                .Where(b => b.BookingTime.TimeOfDay.Hours.Equals(i)).Count();

                var bookingInOneHours = listBooking?
                .Where(b => b.BookingTime.TimeOfDay.Hours.Equals(i) &&
                /*b.TotalEstimatedCompletionTime*/ b.CustomersCanReceiveTheCarTime == 1).Count();

                var test5 = i - current.Hour;

                var test4 = i - current.Hour < 4;
                var test3 = current.Date == dateSelect.Date;

                switch (bookingCount)
                {
                    case var bookingCout when i - current.Hour < 4 &&
                    (current.Date == dateSelect.Date):
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
                                var durationFirstHour = listBooking?.Where(b => b.BookingTime.TimeOfDay.Hours.Equals(i) &&
                                 /*b.TotalEstimatedCompletionTime*/ b.CustomersCanReceiveTheCarTime > 1).FirstOrDefault()!./*TotalEstimatedCompletionTime*/CustomersCanReceiveTheCarTime;
                                if (durationFirstHour > 1)
                                {
                                    for (int b = i + 1; b < i + durationFirstHour; b++)
                                    {
                                        UpdateListHours(b, listHours);
                                    }
                                    z = (int)durationFirstHour;
                                    i = i + z - 1;
                                    var isBookin = listBooking?.Where(b => b.BookingTime.TimeOfDay.Hours.Equals(i + 1) && /*b.TotalEstimatedCompletionTime*/ b.CustomersCanReceiveTheCarTime > 1).Count();
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

        public async Task CreateForManager(BookingCreateForManagerRequestDto requestDto)
        {
            try
            {
                var watch = System.Diagnostics.Stopwatch.StartNew();

                var bookingTime = DateOnly.Parse(requestDto.DateSelected).ToDateTime(TimeOnly.Parse(requestDto.TimeSelected));
                var currentDay = DateTime.Now;

                var garage = await garageRepository.GetGarage(requestDto.GarageId);
                var isCarExist = await carRepository.IsCarExist(requestDto.CarId);

                var totalEstimated = 0;

                for (int i = 0; i < requestDto.ServiceList!.Count; i++)
                {
                    var serviceDuration = await serviceRepository.GetDuration(requestDto.ServiceList[i].ServiceDetailId);
                    totalEstimated += serviceDuration;
                }

                var bookingAt = bookingTime.TimeOfDay.Hours;

                var bookingCheck = new BookingCheckRequestDto { DateSelected = requestDto.DateSelected, GarageId = requestDto.GarageId };
                var listHours = await IsBookingAvailable(bookingCheck);
                var isAvailableHours = listHours.FirstOrDefault(l => DateTime.Parse(l.Hour).TimeOfDay.Hours == bookingAt);

                var openAt = DateTime.ParseExact(garage!.OpenAt, "hh:mm tt", CultureInfo.InvariantCulture).TimeOfDay.Hours;
                var closeAt = DateTime.ParseExact(garage!.CloseAt, "hh:mm tt", CultureInfo.InvariantCulture).TimeOfDay.Hours;

                switch (false)
                {
                    case var isNull when isNull == (requestDto.CarId != 0):
                        throw new MyException("Car ID không được null!", 404);
                    case var isExist when isExist == isCarExist:
                        throw new MyException("Xin lỗi xe không tồn tại.", 404);
                    case var isTime when isTime == (bookingTime >= currentDay):
                        throw new MyException("Ngày được chọn phải lớn hơn hoặc trùng ngày hiện tại.", 404);
                    case var isEmpty when isEmpty == (requestDto.ServiceList.Count > 0):
                        throw new MyException("Phải chọn ít nhất một dịch vụ trước khi đặt đơn.", 404);
                    case var isFalse when isFalse == (openAt <= bookingAt && bookingAt <= closeAt):
                        throw new MyException("Xin lỗi khung giờ không trong khung giờ làm việc.", 404);
                    case var isMany when isMany == (requestDto.ServiceList.Count <= 3):
                        throw new MyException("Chỉ đặt được tối đa là 3 dịch vụ.", 404);
                }

                await RunManager(requestDto, bookingTime, totalEstimated);

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

        private async Task RunManager(BookingCreateForManagerRequestDto requestDto, DateTime bookingTime, int totalEstimated)
        {
            try
            {
                var booking = mapper.Map<BookingCreateForManagerRequestDto, Booking>(requestDto,
                otp => otp.AfterMap((src, des) =>
                {
                    des.BookingTime = bookingTime;
                }));

                var bookingId = await bookingRepository.Create(booking);

                //--------------------------------------------------------------------------------------------------
                decimal discountedPrice = 0;
                decimal originalPrice = 0;
                decimal totalPrice = 0;

                var listService = requestDto.ServiceList;
                var listBookingDetail = new List<BookingDetail>();

                for (int i = 0; i < listService!.Count; i++)
                {
                    //get default product id
                    decimal productPrice = 0;
                    decimal servicePrice = serviceRepository.GetPrice(listService[i].ServiceDetailId);

                    var product = new Product();
                    if (listService[i].ProductId > 0)
                    {
                        product = productRepository.GetDefaultProduct(listService[i].ProductId);
                    }

                    if (product is not null)
                    {
                        productPrice = product.ProductPrice;
                        /*product.ProductQuantity--;
                        await productRepository.Update(product);*/
                    }

                    originalPrice += productPrice + servicePrice;

                    var bookingDetail = new BookingDetail()
                    {
                        ProductPrice = productPrice,
                        ServicePrice = servicePrice,
                        BookingServiceStatus = BookingServiceStatus.NotStart,
                        ProductId = product is null ? null : product.ProductId,
                        ServiceDetailId = listService[i].ServiceDetailId,
                        //MechanicId = mechanicId,
                        BookingId = bookingId
                    };

                    listBookingDetail.Add(bookingDetail);
                }

                await bookingDetailRepository.Create(listBookingDetail);

                totalPrice = originalPrice;

                //--------------------------------------------------------------------------------------------------

                booking.OriginalPrice = originalPrice;
                booking.DiscountPrice = discountedPrice;
                booking.TotalPrice = totalPrice;
                booking.FinalPrice = totalPrice;
                booking.TotalEstimatedCompletionTime = totalEstimated;
                booking.CustomersCanReceiveTheCarTime = totalEstimated + 1;

                //await GenerateQRCode(booking);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<PaymentLinkDto> Create(BookingCreateRequestDto requestDto)
        {
            try
            {
                var watch = System.Diagnostics.Stopwatch.StartNew();

                var response = new PaymentLinkDto();

                var bookingTime = DateOnly.Parse(requestDto.DateSelected).ToDateTime(TimeOnly.Parse(requestDto.TimeSelected));
                var currentDay = DateTime.Now;

                var garage = await garageRepository.GetGarage(requestDto.GarageId);
                var isCarExist = await carRepository.IsCarExist(requestDto.CarId);
                var isCarAvalible = await carRepository.IsCarAvalible(requestDto.CarId);

                if (requestDto.CouponId > 0)
                {
                    if (!await couponRepository.IsCouponExist(requestDto.CouponId))
                    {
                        throw new MyException("The coupon doesn't exist.", 404);
                    }
                }

                var totalEstimated = 0;

                for (int i = 0; i < requestDto.ServiceList!.Count; i++)
                {
                    var serviceDuration = await serviceRepository.GetDuration(requestDto.ServiceList[i]);
                    totalEstimated += serviceDuration;
                }

                var bookingAt = bookingTime.TimeOfDay.Hours;

                var bookingCheck = new BookingCheckRequestDto { DateSelected = requestDto.DateSelected, GarageId = requestDto.GarageId };
                var listHours = await IsBookingAvailable(bookingCheck);
                var isAvailableHours = listHours.FirstOrDefault(l => DateTime.Parse(l.Hour).TimeOfDay.Hours == bookingAt);

                var openAt = DateTime.ParseExact(garage!.OpenAt, "hh:mm tt", CultureInfo.InvariantCulture).TimeOfDay.Hours;
                var closeAt = DateTime.ParseExact(garage!.CloseAt, "hh:mm tt", CultureInfo.InvariantCulture).TimeOfDay.Hours;

                switch (false)
                {
                    case var isNull when isNull == (requestDto.CarId != 0):
                        throw new MyException("Car ID không được null!", 404);
                    case var isAvalible when isAvalible == isCarAvalible:
                        throw new MyException("Xin lỗi xe của bạn không khả dụng.", 404);
                    case var isExist when isExist == isCarExist:
                        throw new MyException("Xin lỗi xe của bạn không tồn tại.", 404);
                    case var isTime when isTime == (bookingTime >= currentDay):
                        throw new MyException("Ngày được chọn phải lớn hơn hoặc trùng ngày hiện tại.", 404);
                    case var isEmpty when isEmpty == (requestDto.ServiceList.Count > 0):
                        throw new MyException("Phải chọn ít nhất một dịch vụ trước khi đặt đơn.", 404);
                    case var isFalse when isFalse == (openAt <= bookingAt && bookingAt <= closeAt):
                        throw new MyException("Xin lỗi khung giờ không trong khung giờ làm việc.", 404);
                    case var isMany when isMany == (requestDto.ServiceList.Count <= 3):
                        throw new MyException("Chỉ đặt được tối đa là 3 dịch vụ.", 404);
                }

                var listBookingCount = await bookingRepository.CountBookingByTimePerDay(bookingTime, requestDto.GarageId);
                var lotCount = garage!.Lots.Count;

                if (lotCount - listBookingCount == 1)
                {
                    var getGarageVersionNumber = await garageRepository.GetGarageVersionNumber(requestDto.GarageId);
                    Debug.WriteLine($"{System.Text.Encoding.Default.GetString(garage!.VersionNumber)}");
                    if (getGarageVersionNumber!.SequenceEqual(garage!.VersionNumber))
                    {
                        await garageRepository.Update(garage);
                        response = await Run(requestDto, bookingTime, totalEstimated);
                    }
                    else
                    {
                        throw new MyException("Xin lỗi, đã có ai đó đặt đơn hàng trước bạn.", 409);
                    }
                }
                else
                {
                    response = await Run(requestDto, bookingTime, totalEstimated);
                }

                watch.Stop();
                Debug.WriteLine($"\nTotal run time (Milliseconds) Create(): {watch.ElapsedMilliseconds}\n");

                return response;
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

        private static string GenerateRandomString()
        {
            const string chars = "0123456789ABCDEF";
            var random = new Random();

            var result = new StringBuilder();
            result.Append(random.Next(10, 100)); // Two random digits
            result.Append(chars[random.Next(chars.Length)]); // One random character
            result.Append(random.Next(10, 100)); // Two random digits
            result.Append(chars[random.Next(chars.Length)]); // One random character
            result.Append(chars[random.Next(chars.Length)]); // One random character
            result.Append(random.Next(10, 100)); // Two random digits
            result.Append(chars[random.Next(chars.Length)]); // One random character

            return result.ToString();
        }

        private async Task<PaymentLinkDto> Run(BookingCreateRequestDto requestDto, DateTime bookingTime, int totalEstimated)
        {
            try
            {
                var booking = mapper.Map<BookingCreateRequestDto, Booking>(requestDto,
                otp => otp.AfterMap((src, des) =>
                {
                    des.BookingTime = bookingTime;
                }));

                var bookingId = await bookingRepository.Create(booking);

                var checkOutDto = new CheckOutRequestDto() { ServiceList = requestDto.ServiceList, CouponId = requestDto.CouponId };

                var checkOut = await RunCheckOut(checkOutDto, true, bookingId, requestDto.MechanicId);

                if (requestDto.MechanicId != 0)
                {
                    var bookingMechanic = new BookingMechanic()
                    {
                        WorkingDate = bookingTime,
                        BookingId = bookingId,
                        MechanicId = requestDto.MechanicId
                    };

                    await mechanicRepository.CreateBookingMechanic(bookingMechanic);
                }

                booking.OriginalPrice = checkOut.Item1;
                booking.DiscountPrice = checkOut.Item2;
                booking.TotalPrice = checkOut.Item3;
                booking.FinalPrice = checkOut.Item3 - 100;
                booking.TotalEstimatedCompletionTime = totalEstimated;
                booking.CustomersCanReceiveTheCarTime = totalEstimated + 1;

                //await GenerateQRCode(booking);

                var payment = new PaymentRequest()
                {
                    CarId = requestDto.CarId,
                    BookingId = bookingId,
                    PaymentId = booking.BookingCode,
                    PaymentContent = $"Thanh toán đơn hàng: {booking.BookingCode}",
                    PaymentRefId = GenerateRandomString(),
                    RequiredAmount = 100000,
                };

                var response = await iVNPayPaymentGateway.Create(payment);

                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private async Task<(decimal, decimal, decimal)> RunCheckOut(CheckOutRequestDto requestDto, bool isCreateNew, int? bookingId, int? mechanicId)
        {
            try
            {
                switch (false)
                {
                    case var isFalse when isFalse == requestDto.ServiceList.Any(s => s > 0):
                        throw new MyException("Id can't take 0 value!", 404);
                    case var isFalse when isFalse == (requestDto.CouponId != 0):
                        requestDto.CouponId = null;
                        break;
                }

                decimal discountedPrice = 0;
                decimal originalPrice = 0;
                decimal totalPrice = 0;

                var listService = requestDto.ServiceList;
                var listBookingDetail = new List<BookingDetail>();

                for (int i = 0; i < listService.Count; i++)
                {
                    //get default product id
                    decimal productPrice = 0;
                    decimal servicePrice = serviceRepository.GetPrice(listService[i]);
                    var product = productRepository.GetDefaultProduct(listService[i]);

                    if (product is not null)
                    {
                        productPrice = product.ProductPrice;
                    }

                    originalPrice += productPrice + servicePrice;

                    if (isCreateNew == true)
                    {
                        /*if (product is not null)
                        {
                            product.ProductQuantity--;
                            await productRepository.Update(product);
                        }*/

                        var bookingDetail = new BookingDetail()
                        {
                            ProductPrice = productPrice,
                            ServicePrice = servicePrice,
                            BookingServiceStatus = BookingServiceStatus.NotStart,
                            ProductId = product is null ? null : product.ProductId,
                            ServiceDetailId = listService[i],
                            //MechanicId = mechanicId,
                            BookingId = bookingId
                        };

                        listBookingDetail.Add(bookingDetail);
                    }
                }

                if (isCreateNew == true)
                {
                    await bookingDetailRepository.Create(listBookingDetail);
                }

                if (requestDto.CouponId is not null)
                {
                    var coupon = await couponRepository.Detail(requestDto.CouponId);

                    switch (coupon!.CouponType)
                    {
                        case CouponType.Percent:
                            discountedPrice = originalPrice * (coupon.CouponValue / 100);
                            totalPrice = originalPrice - discountedPrice;
                            break;
                        case CouponType.FixedAmount:
                            discountedPrice = coupon.CouponValue;
                            totalPrice = originalPrice - discountedPrice;
                            break;
                    }

                    coupon.NumberOfTimesToUse--;
                    await couponRepository.Update(coupon);
                }
                else
                {
                    totalPrice = originalPrice;
                }

                return (originalPrice, discountedPrice, totalPrice);
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public async Task<CheckOutResponseDto> CheckOut(CheckOutRequestDto requestDto)
        {
            try
            {
                var checkOut = await RunCheckOut(requestDto, false, null, null);

                return new CheckOutResponseDto
                {
                    OriginalPrice = FormatCurrency.FormatNumber(checkOut.Item1) + " VND",
                    DiscountedPrice = FormatCurrency.FormatNumber(checkOut.Item2) + " VND",
                    TotalPrice = FormatCurrency.FormatNumber(checkOut.Item3) + " VND",
                    Deposit = FormatCurrency.FormatNumber(100) + " VND",
                };
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

        public async Task UpdateBookingDetailForManager(int bookingDetailId, int productId)
        {
            try
            {
                var bookingDetailList = new List<BookingDetail>();

                var bookingDetail = await bookingDetailRepository.Detail(bookingDetailId);

                if (bookingDetail is not null)
                {
                    var productOld = bookingDetail.Product;

                    var productNew = await productRepository.Detail(productId);

                    bookingDetail.ProductId = productNew!.ProductId;
                    bookingDetail.ProductPrice = productNew.ProductPrice;
                    bookingDetail.UpdatedAt = DateTime.Now;
                    bookingDetailList.Add(bookingDetail);

                    await bookingDetailRepository.Update(bookingDetailList);

                    var booking = await bookingRepository.Detail((int)bookingDetail.BookingId!);

                    decimal originalPrice = 0;
                    decimal totalPrice = 0;
                    decimal finalPrice = 0;

                    foreach (var bookingDetailItem in booking!.BookingDetails)
                    {
                        originalPrice += bookingDetailItem.ProductPrice + bookingDetailItem.ServicePrice;
                    }

                    totalPrice = originalPrice - booking.DiscountPrice;
                    finalPrice = totalPrice - 100;

                    booking.OriginalPrice = originalPrice;
                    booking.TotalPrice = totalPrice;
                    booking.FinalPrice = finalPrice;

                    await bookingRepository.Update(booking);
                }
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

        public async Task ConfirmAcceptedBooking(bool isAccepted, int bookingId)
        {
            try
            {
                if (isAccepted is true)
                {
                    var booking = await bookingRepository.Detail(bookingId);
                    booking!.IsAccepted = isAccepted;

                    await bookingRepository.Update(booking);
                }
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

        public async Task UpdateStatus(int bookingId, BookingStatus bookingStatus, int userId)
        {
            try
            {
                var watch = Stopwatch.StartNew();

                var booking = await bookingRepository.Detail(bookingId)
                ?? throw new MyException("Đơn hàng không tồn tại.", 404);

                switch (bookingStatus)
                {
                    case BookingStatus.Canceled:
                        switch (false)
                        {
                            case var isFalse when isFalse == (booking.BookingStatus == BookingStatus.Pending):
                                throw new MyException("Đơn hàng chỉ có thể được hủy khi đang ở trạng thái chờ được xử lí.", 404);
                        }

                        var _car = await carRepository.Detail(booking.Car.CarId);

                        _car!.CarBookingStatus = CarStatus.Available;

                        await carRepository.Update(_car);

                        var roleId = await bookingRepository.GetRole(userId);

                        if (roleId == 2 ||
                        roleId == 4)
                        {
                            await fCMSendNotificationMobile.SendMessagesToSpecificDevices
                            (booking.Car.Customer.User.DeviceToken, "Thông báo:",
                            "Đơn hàng đã hủy bởi Garage hãy liên hệ Garage để biết chi tiết.");
                        }

                        var _timeZone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");
                        var _isEditAble = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow.AddMinutes(40), _timeZone);

                        var _bookingTime = TimeZoneInfo.ConvertTimeFromUtc(booking.BookingTime, _timeZone);

                        var _current = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, _timeZone);

                        if (!(DateTime.Now.Day == booking.BookingTime.Day))
                        {
                            await fCMSendNotificationMobile.SendMessagesToSpecificDevices
                            (booking.Car.Customer.User.DeviceToken, "Thông báo:",
                            "Đơn hàng của bạn sẽ được hoàn tiền, hãy liên hệ với Garage để được hoàn tiền đặt trước.");
                        }
                        else
                        {
                            await fCMSendNotificationMobile.SendMessagesToSpecificDevices
                            (booking.Car.Customer.User.DeviceToken, "Thông báo:",
                            "Xin lỗi đơn hàng của bạn sẽ không được hoàn tiền vì đã quá 4 tiếng trước lúc Check-in.");
                        }

                        break;
                    case BookingStatus.CheckIn:
                        // var timeZone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");
                        // var isEditAble = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow.AddMinutes(40), timeZone);

                        // var bookingTime = TimeZoneInfo.ConvertTimeFromUtc(booking.BookingTime, timeZone);

                        // var current = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, timeZone);

                        if (!(DateTime.Now.Day == booking.BookingTime.Day))
                        {
                            throw new MyException("Xin lỗi đơn hàng của bạn chưa thể Check-in vào lúc này.", 404);
                        }

                        switch (false)
                        {
                            case var isFalse when isFalse == (booking.BookingStatus == BookingStatus.Pending):
                                throw new MyException("Đơn hàng chỉ có thể được check-in khi đang ở trạng thái chờ được xử lí.", 404);
                        }

                        await UpdateLotStatus(LotStatus.Assigned, booking!);

                        await fCMSendNotificationMobile.SendMessagesToSpecificDevices
                        (booking.Car.Customer.User.DeviceToken, "Thông báo:", "Đơn của bạn đã được Check-in.");

                        break;
                    case BookingStatus.Completed:

                        switch (false)
                        {
                            case var isAll when isAll == (!booking!.BookingDetails.All(b => b.BookingServiceStatus == BookingServiceStatus.NotStart)):
                                throw new MyException("Tất cả dịch vụ cần phải được hoàn tất.", 404);
                            case var isAccepted when isAccepted == (booking.IsAccepted is true):
                                throw new MyException("Đơn hàng vẫn chưa được chấp nhận bởi người chủ xe.", 404);
                            case var isFalse when isFalse == (booking.BookingStatus == BookingStatus.CheckIn):
                                throw new MyException("Đơn hàng chỉ có thể được hoàn thành khi đang ở trạng thái đang được xử lí.", 404);
                        }

                        var bookingDetails = await bookingDetailRepository.FilterBookingDetailByBookingId(bookingId);

                        foreach (var bookingDetail in bookingDetails)
                        {
                            if (bookingDetail.BookingServiceStatus == BookingServiceStatus.Error &&
                            !bookingDetails.Any(b => b.BookingServiceStatus == BookingServiceStatus.NotStart))
                            {
                                booking!.FinalPrice -= bookingDetail.ServicePrice;
                            }
                        }

                        var car = await carRepository.Detail(booking.Car.CarId);

                        car!.CarBookingStatus = CarStatus.Available;

                        await carRepository.Update(car);

                        await UpdateLotStatus(LotStatus.Free, booking!);

                        var mechanicList = await mechanicRepository.GetMechanicByBooking(bookingId);

                        var mechanicUpdateStatusList = mechanicList
                        .Select(m => { m.MechanicStatus = MechanicStatus.Available; return m; }).ToList();

                        foreach (var item in mechanicUpdateStatusList)
                        {
                            var bookingMechanic = await mechanicRepository.DetailBookingMechanic(item.MechanicId, bookingId);

                            if (bookingMechanic is not null)
                            {
                                bookingMechanic!.BookingMechanicStatus = Status.Deactivate;
                                await mechanicRepository.UpdateBookingMechanic(bookingMechanic);
                            }
                            else
                            {
                                throw new MyException("Thợ không tồn tại", 404);
                            }

                            await mechanicRepository.Update(item);
                        }

                        await fCMSendNotificationMobile.SendMessagesToSpecificDevices
                        (booking.Car.Customer.User.DeviceToken, "Thông báo:", "Đơn của bạn đã hoàn tất.");

                        break;
                    case BookingStatus.CheckOut:

                        switch (false)
                        {
                            case var isFalse when isFalse == (booking.BookingStatus == BookingStatus.Completed):
                                throw new MyException("Đơn hàng chỉ có thể được check-out khi đang ở trạng thái hoàn thành.", 404);
                        }

                        break;
                }

                booking!.BookingStatus = bookingStatus;
                booking.UpdatedAt = DateTime.Now;

                await bookingRepository.Update(booking);

                watch.Stop();
                Debug.WriteLine($"Total run time (Milliseconds) Run(): {watch.ElapsedMilliseconds}");
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

        private async Task UpdateLotStatus(LotStatus status, Booking booking)
        {
            switch (status)
            {
                case LotStatus.Assigned:
                    var lotFree = await lotRepository.GetFreeLotInGarage((int)booking.GarageId!)
                    ?? throw new MyException("Xin lỗi hiện tại Ga-ra không còn đủ chỗ để check-in thêm.", 404);

                    var licensePlate = await carRepository.GetLicensePlate((int)booking.CarId!);

                    lotFree!.LotStatus = LotStatus.BeingUsed;
                    lotFree.IsAssignedFor = licensePlate;

                    await AssigneMechanicForBooking(booking);

                    await lotRepository.Update(lotFree);
                    break;
                case LotStatus.Free:
                    var lot = await lotRepository.GetLotByLicensePlate((int)booking.GarageId!, booking.Car.CarLicensePlate)
                    ?? throw new MyException("Xin lỗi hệ thống không tìm thấy xe của bạn.", 404);

                    lot.LotStatus = LotStatus.Free;
                    lot.IsAssignedFor = null;

                    await lotRepository.Update(lot);
                    break;
            }
        }

        private async Task AssigneMechanicForBooking(Booking booking)
        {
            var isCustomerPickMainMechanic = await mechanicRepository.IsCustomerPickMainMechanic(booking.BookingTime);

            var mechanicList = await mechanicRepository.FilterMechanicsAvailableByGarage((int)booking.GarageId!, false);

            var numService = booking.BookingDetails.Count;

            //var testOnly = mechanicList.OrderBy(m => m.BookingMechanics.Count).ToList();

            if (mechanicList.Any())
            {
                var pickRandomMechanic = mechanicList.Count >= numService
                ? mechanicList.OrderBy(m => m.BookingMechanics.Count).Take(numService).ToList()
                : mechanicList.OrderBy(m => m.BookingMechanics.Count).ToList();

                if (isCustomerPickMainMechanic is null)
                {
                    var listLv3Mechanic = await mechanicRepository.FilterMechanicsAvailableByGarage((int)booking.GarageId!, true);

                    if (!listLv3Mechanic.Any())
                    {
                        throw new MyException("Garage đang chọn không có thợ bậc 3 rảnh.", 404);
                    }

                    var pickLv3MechanicMinWork = listLv3Mechanic.OrderBy(m => m.BookingMechanics.Count).First();
                    pickRandomMechanic.Add(pickLv3MechanicMinWork);
                }
                else
                {
                    pickRandomMechanic.Add(isCustomerPickMainMechanic.Mechanic);
                }

                foreach (var mechanic in pickRandomMechanic)
                {
                    var bookingMechanic = new BookingMechanic()
                    {
                        WorkingDate = booking.BookingTime,
                        BookingId = booking.BookingId,
                        BookingMechanicStatus = Status.Activate,
                        MechanicId = mechanic.MechanicId
                    };

                    await mechanicRepository.CreateBookingMechanic(bookingMechanic);

                    mechanic.MechanicStatus = MechanicStatus.NotAvailable;

                    await mechanicRepository.Update(mechanic);
                }
            }
            else
            {
                throw new MyException("Garage hiện tại không còn thợ rảnh.", 404);
            }

            /*var bookingDetailList = await bookingDetailRepository.FilterBookingDetailByBookingId(booking.BookingId);
            var mechanicAvailableList = await mechanicRepository.FilterMechanicAvailableByGarageId((int)booking.GarageId!);

            switch (false)
            {
                case var isFalse when isFalse == (bookingDetailList.Count <= mechanicAvailableList.Count):
                    throw new MyException("There are not enough mechanic for booking.", 404);
            }

            var minWorkingHour = mechanicAvailableList.Take(bookingDetailList.Count).ToList();

            //Index out of range bug
            for (int i = 0; i < bookingDetailList.Count; i++)
            {
                //bookingDetailList[i].MechanicId = minWorkingHour[i].MechanicId;
                var estimatedTime = await serviceRepository.GetDuration((int)bookingDetailList[i].ServiceDetail.ServiceId!);
                //minWorkingHour[i].TotalBookingApplied += estimatedTime;
            }

            await bookingDetailRepository.Update(bookingDetailList);*/
        }

        [System.Runtime.Versioning.SupportedOSPlatform("windows")]
        private async Task GenerateQRCode(Booking booking)
        {
            try
            {
                //string url = $"https://carserviceappservice.azurewebsites.net/api/booking/run-qr/{booking.BookingId}";
                string url = $"{booking.BookingId}";

                var qrGenerator = new QRCodeGenerator();
                var qrCodeData = qrGenerator.CreateQrCode(url, QRCodeGenerator.ECCLevel.Q);
                var qrCode = new QRCode(qrCodeData);
                var qrCodeImage = qrCode.GetGraphic(20);

                await using (var stream = new MemoryStream())
                {
                    qrCodeImage.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);

                    byte[] imageBytes = stream.ToArray();
                    var base64String = "data:image/png;base64," + Convert.ToBase64String(imageBytes);

                    byte[] imgBytes = Convert.FromBase64String(base64String.Split(',')[1]);

                    var blobServiceClient = new BlobServiceClient(configuration["BlobStorage:ConnectionString"]!);
                    var blobContainerClient = blobServiceClient.GetBlobContainerClient(configuration["BlobStorage:Container"]!);
                    var blobName = GenerateRandomString() + "_qr_code.jpg";

                    using (var blobstream = new MemoryStream(imgBytes))
                    {
                        stream.Position = 0;
                        blobContainerClient.UploadBlob(blobName, blobstream);
                    }

                    var blobClient = blobContainerClient.GetBlobClient(blobName);

                    booking!.QrImage = blobClient.Uri.ToString();

                    await bookingRepository.Update(booking);
                }

                // string qrCodeImageBase64;
                // await using (var ms = new MemoryStream())
                // {
                //     qrCodeImage.Save(ms, ImageFormat.Png);
                //     byte[] imageBytes = ms.ToArray();
                //     qrCodeImageBase64 = "data:image/png;base64," + Convert.ToBase64String(imageBytes);
                // }

                // string qrCodeImageUrl = qrCodeImageBase64;
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

        public async Task<BookingDetailForStaffResponseDto> RunQRCode(int bookingId, int garageId)
        {
            /*var url = $"https://carserviceappservice.azurewebsites.net/api/booking/update-status-booking/{bookingId}&2";
            var data = "{\"status\": \"updated status\"}";

            using (var httpClient = new HttpClient())
            {
                var request = new HttpRequestMessage(HttpMethod.Put, url)
                {
                    Content = new StringContent(data, System.Text.Encoding.UTF8, "application/json")
                };

                var response = await httpClient.SendAsync(request);
                var statusCode = response.StatusCode;

                Console.WriteLine($"Response Status Code: {statusCode}");
            }*/

            var serviceSelectList = new List<GroupServiceBookingDetailDto>();

            var serviceGroupList = new List<string> { };

            var booking = await bookingRepository.DetailBookingForCustomer(bookingId);

            if (booking is null)
            {
                throw new MyException("Booking is not exist!", 404);
            }

            if (booking.GarageId != garageId)
            {
                throw new MyException("Xin lỗi đơn hàng này không thuộc Garage hiện tại.", 404);
            }

            var listBookingDetails = await serviceRepository.GetServiceForBookingDetail(bookingId);

            foreach (var item in listBookingDetails)
            {
                serviceGroupList.Add(item.ServiceDetail.Service.ServiceGroup);
            }

            foreach (var item in serviceGroupList.Distinct())
            {
                var serviceList = listBookingDetails.Where(s => s.ServiceDetail.Service.ServiceGroup.Equals(item)).ToList();

                var serviceDtoList = mapper.Map<List<BookingDetail>, List<ServiceListBookingDetailDto>>(serviceList,
                    obj => obj.AfterMap((src, des) =>
                    {
                        for (int i = 0; i < src.Count; i++)
                        {
                            if (src[i].Product is not null)
                            {
                                var serviceName = src[i].ServiceDetail.Service.ServiceName + "@Sản phẩm đi kèm: " + src[i].Product.ProductName;
                                serviceName = serviceName.Replace("@", Environment.NewLine);
                                var price = FormatCurrency.FormatNumber(src[i].ServicePrice) + " VND" + "@"
                                + FormatCurrency.FormatNumber(src[i].ProductPrice) + " VND";
                                price = price.Replace("@", Environment.NewLine);

                                des[i].ServiceName = serviceName;
                                des[i].ServicePrice = price;
                            }
                            else
                            {
                                des[i].ServiceName = src[i].ServiceDetail.Service.ServiceName;
                                des[i].ServicePrice = FormatCurrency.FormatNumber(src[i].ServicePrice) + " VND";
                            }
                        }
                    }));
                serviceSelectList.Add(new GroupServiceBookingDetailDto { ServiceGroup = item, ServiceListBookingDetailDtos = serviceDtoList });


            }

            var bookingDto = mapper.Map<BookingDetailForStaffResponseDto>(booking,
                obj => obj.AfterMap((src, des) =>
                {
                    des.groupServiceBookingDetailDtos = serviceSelectList;
                }));

            return bookingDto;
        }

        public async Task<BookingRevenueResponseDto> CountRevune(int garageId)
        {
            try
            {
                var isGarageExist = await garageRepository.IsGarageExist(garageId);

                switch (false)
                {
                    case var isExist when isExist == isGarageExist:
                        throw new MyException("The garage doesn't exist.", 404);
                }

                (var amountEarned, var serviceEarned,
                var productEarned, var sumPaid, var sumUnpaid,
                var countPaid, var countUnpaid, var checkInCount, var checkOutCount) = await bookingRepository.CountRevenue(garageId);

                var revenue = new BookingRevenueResponseDto
                {
                    AmountEarned = FormatCurrency.FormatNumber(amountEarned) + " VND",
                    ServiceEarned = FormatCurrency.FormatNumber(serviceEarned) + " VND",
                    ProductEarned = FormatCurrency.FormatNumber(productEarned) + " VND",
                    SumPaid = FormatCurrency.FormatNumber(sumPaid) + " VND",
                    SumUnPaid = FormatCurrency.FormatNumber(sumUnpaid) + " VND",
                    CountPaid = countPaid,
                    CountUnpaid = countUnpaid,
                    CountCheckInt = checkInCount,
                    CountCheckOut = checkOutCount
                };

                return revenue;
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

        public async Task<List<FilterByBookingStatusResponseDto>> FilterBookingByStatusCustomer(int bookingStatus, int userId)
        {
            try
            {
                if (!typeof(BookingStatus).IsEnumDefined(bookingStatus))
                {
                    throw new MyException("The status number is out of avaliable range.", 404);
                }

                var list = await bookingRepository.FilterBookingByStatusCustomer(bookingStatus, userId);

                var listDto = mapper.Map<List<FilterByBookingStatusResponseDto>>(list);

                return listDto;
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

        public async Task<BookingDetailForCustomerResponseDto> DetailBookingForCustomer(int bookingId)
        {
            try
            {
                var isBookingExist = await bookingRepository.IsBookingExist(bookingId);

                switch (false)
                {
                    case var isExist when isExist == isBookingExist:
                        throw new MyException("The booking doesn't exist.", 404);
                }

                var serviceSelectList = new List<GroupServiceBookingDetailDto>();

                var serviceGroupList = new List<string> { };

                var booking = await bookingRepository.DetailBookingForCustomer(bookingId);

                var listBookingDetails = await serviceRepository.GetServiceForBookingDetail(bookingId);

                foreach (var item in listBookingDetails)
                {
                    serviceGroupList.Add(item.ServiceDetail.Service.ServiceGroup);
                }

                foreach (var item in serviceGroupList.Distinct())
                {
                    var serviceList = listBookingDetails.Where(s => s.ServiceDetail.Service.ServiceGroup.Equals(item)).ToList();

                    var serviceDtoList = mapper.Map<List<BookingDetail>, List<ServiceListBookingDetailDto>>(serviceList,
                    obj => obj.AfterMap((src, des) =>
                    {
                        for (int i = 0; i < src.Count; i++)
                        {
                            if (src[i].Product is not null)
                            {
                                var serviceName = src[i].ServiceDetail.Service.ServiceName + "@Sản phẩm đi kèm: " + src[i].Product.ProductName;
                                serviceName = serviceName.Replace("@", System.Environment.NewLine);
                                var price = FormatCurrency.FormatNumber(src[i].ServicePrice) + " VND" + "@"
                                + FormatCurrency.FormatNumber(src[i].ProductPrice) + " VND";
                                price = price.Replace("@", System.Environment.NewLine);

                                des[i].ServiceName = serviceName;
                                des[i].ServicePrice = price;
                            }
                            else
                            {
                                des[i].ServiceName = src[i].ServiceDetail.Service.ServiceName;
                                des[i].ServicePrice = FormatCurrency.FormatNumber(src[i].ServicePrice) + " VND";
                            }
                        }
                    }));

                    serviceSelectList.Add(new GroupServiceBookingDetailDto { ServiceGroup = item, ServiceListBookingDetailDtos = serviceDtoList });
                }

                var bookingDto = mapper.Map<BookingDetailForCustomerResponseDto>(booking,
                obj => obj.AfterMap((src, des) =>
                {
                    des.GroupServiceBookingDetailDtos = serviceSelectList;
                }));

                return bookingDto;
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

        public async Task<List<HourDto>> FilterListBookingByGarageAndDate(int bookingId, string date)
        {
            try
            {
                var dateSelect = DateTime.Parse(date);

                var list = await bookingRepository.FilterListBookingByGarageAndDate(bookingId, dateSelect);

                var listHours = new List<HourDto>();

                foreach (var booking in list)
                {
                    if (!listHours.Any(b => b.Hour.Equals(booking.BookingTime.ToString("hh:tt"))))
                    {
                        var bookingPerHour = list.Where(b => b.BookingTime.Equals(booking.BookingTime)).ToList();

                        var listDto = mapper.Map<List<BookingListForStaffResponseDto>>(bookingPerHour);

                        var hour = new HourDto() { Hour = booking.BookingTime.ToString("hh:tt"), BookingListForStaffResponseDtos = listDto };

                        listHours.Add(hour);
                    }
                }

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

        public async Task<BookingCountResponseDto> CountBookingPerStatus(int? garageId)
        {
            try
            {
                var count = await bookingRepository.CountBookingPerStatus(garageId);

                var countDto = new BookingCountResponseDto()
                {
                    Pending = count.Item1,
                    Canceled = count.Item2,
                    CheckIn = count.Item3,
                    Completed = count.Item4
                };

                return countDto;
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

        public async Task UpdateBookingDetailStatus(int bookingDetailId, int status)
        {
            try
            {
                var bookingDetailList = new List<BookingDetail>();
                var bookingDetail = await bookingDetailRepository.Detail(bookingDetailId);

                switch (false)
                {
                    case var isExist when isExist == (bookingDetail is not null):
                        throw new MyException("The booking detail doesn't exist.", 404);
                    case var isFalse when isFalse == typeof(BookingServiceStatus).IsEnumDefined(status!):
                        throw new MyException("The status number is out of avaliable range.", 404);
                    case var isFalse when isFalse != ((bookingDetail!.BookingServiceStatus != 0) && status == 0):
                        throw new MyException("Không thể chuyển trạng thái lỗi hoặc xong thành chưa bắt đầu!", 404);
                }

                var booking = await bookingRepository.Detail((int)bookingDetail.BookingId!)
                ?? throw new MyException("Đơn hàng không tồn tại.", 404);

                if (status == 2)
                {
                    await fCMSendNotificationMobile.SendMessagesToSpecificDevices
                        (booking.Car.Customer.User.DeviceToken, "Thông báo:",
                        $"Đơn của bạn đã xảy ra lỗi ở {bookingDetail.ServiceDetail.Service.ServiceName}.");
                }
                else
                {
                    await fCMSendNotificationMobile.SendMessagesToSpecificDevices
                        (booking.Car.Customer.User.DeviceToken, "Thông báo:",
                        $"Đơn của bạn đã hoàn thành {bookingDetail.ServiceDetail.Service.ServiceName}.");
                }

                bookingDetail.BookingServiceStatus = (BookingServiceStatus)status;
                bookingDetail.UpdatedAt = DateTime.Now;
                bookingDetailList.Add(bookingDetail);

                await bookingDetailRepository.Update(bookingDetailList);
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

        public async Task ConfirmBookingArePaid(int bookingId)
        {
            try
            {
                var booking = await bookingRepository.Detail(bookingId);

                if (!booking!.BookingStatus.Equals(BookingStatus.Completed))
                {
                    throw new MyException("Đơn hàng phải được hoàn thành trước khi thanh toán.", 404);
                }

                await bookingRepository.ConfirmBookingArePaid(bookingId);
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