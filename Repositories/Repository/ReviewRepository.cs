using GraduationThesis_CarServices.Enum;
using GraduationThesis_CarServices.Models;
using GraduationThesis_CarServices.Models.DTO.Page;
using GraduationThesis_CarServices.Models.Entity;
using GraduationThesis_CarServices.Paging;
using GraduationThesis_CarServices.Repositories.IRepository;
using Microsoft.EntityFrameworkCore;

namespace GraduationThesis_CarServices.Repositories.Repository
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly DataContext context;
        public ReviewRepository(DataContext context)
        {
            this.context = context;
        }

        public async Task<(List<Review>?, int count)> View(PageDto page)
        {
            try
            {
                var query = context.Reviews.AsQueryable();

                var count = await query.CountAsync();

                var list = await PagingConfiguration<Review>.Get(query.Include(r => r.Garage)
                .Include(r => r.Customer).ThenInclude(r => r.User), page);

                return (list, count);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<(List<Review>?, int count)> FilterReviewByGarage(int garageId, PageDto page)
        {
            try
            {
                var query = context.Reviews.Where(r => r.GarageId == garageId).AsQueryable();

                var count = await query.CountAsync();
                
                var list = await PagingConfiguration<Review>.Get(query.Include(r => r.Customer).ThenInclude(c => c.User), page);

                return (list, count);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Review?> Detail(int id)
        {
            try
            {
                var review = await context.Reviews
                .Where(r => r.ReviewId == id)
                .Include(r => r.Customer).ThenInclude(c => c.User)
                .Include(r => r.Garage)
                .ThenInclude(c => c.User).FirstOrDefaultAsync();

                return review;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task Create(Review review)
        {
            try
            {
                context.Reviews.Add(review);
                await context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task Update(Review review)
        {
            try
            {
                context.Reviews.Update(review);
                await context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<Review>?> FilterAllReview(int? garageId, DateTime? dateFrom, DateTime? dateTo, PageDto page)
        {
            try
            {
                IQueryable<Review>? runQuery = null;
                var mainQuery = context.Reviews.AsQueryable();

                if (garageId > 0 && garageId is not null)
                {
                    runQuery = mainQuery.Where(r => r.GarageId == garageId).AsQueryable();
                    if (dateFrom is not null && dateTo is not null)
                    {
                        runQuery = mainQuery.Where(r => r.GarageId == garageId & dateFrom <= r.CreatedAt & r.CreatedAt <= dateTo).AsQueryable();
                    }
                    else
                    {
                        if (dateFrom is not null)
                        {
                            runQuery = mainQuery.Where(r => r.GarageId == garageId & dateFrom <= r.CreatedAt).AsQueryable();
                        }
                        if (dateTo is not null)
                        {
                            runQuery = mainQuery.Where(r => r.GarageId == garageId & r.CreatedAt <= dateTo).AsQueryable();
                        }
                    }
                }
                else
                {
                    if (dateFrom is not null && dateTo is not null)
                    {
                        runQuery = mainQuery.Where(r => dateFrom <= r.CreatedAt & r.CreatedAt <= dateTo).AsQueryable();
                    }
                    if (dateFrom is not null)
                    {
                        runQuery = mainQuery.Where(r => dateFrom <= r.CreatedAt).AsQueryable();
                    }
                    if (dateTo is not null)
                    {
                        runQuery = mainQuery.Where(r => r.CreatedAt <= dateTo).AsQueryable();
                    }
                }

                var list = await runQuery!.Include(r => r.Garage).Include(r => r.Customer).ThenInclude(c => c.User).ToListAsync();
                return list;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<(List<Review>, int count)> SearchByName(string searchString, PageDto page)
        {
            try
            {
                var searchTrim = searchString.Trim().Replace(" ", "").ToLower();

                var query = context.Reviews.Include(r => r.Customer).ThenInclude(c => c.User)
                .Where(r => (r.Customer.User.UserFirstName.ToLower().Trim() + r.Customer.User.UserLastName.ToLower().Trim()).Contains(searchTrim)
                || r.Customer.User.UserFirstName.ToLower().Contains(searchTrim)
                || r.Customer.User.UserLastName.ToLower().Contains(searchTrim)).AsQueryable();

                var count = await query.CountAsync();

                var list = await PagingConfiguration<Review>.Get(query.Include(r => r.Garage), page);

                return (list, count);
            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }
}