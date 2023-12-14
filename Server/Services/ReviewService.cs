using BlazorEC.Server.Data;
using BlazorEC.Shared.Entities;
using Microsoft.EntityFrameworkCore;

namespace BlazorEC.Server.Services;

public interface IReviewService
{
    ValueTask<Review> GetAsync(int id);
    ValueTask<List<Review>> FilterByProductIdAsync(int productId);
    ValueTask<int> PostAsync(Review review, Guid userId);
    ValueTask<int> PutAsync(Review review, Guid userId);
    ValueTask<int> DeleteAsync(int id, Guid userId);
}

public class ReviewService : IReviewService
{
    private readonly DataContext context;

    public ReviewService(IDbContextFactory<DataContext> factory)
        => context = factory.CreateDbContext();

    public async ValueTask<Review> GetAsync(int id)
    {
        using (context)
        {
            return await context.Reviews.FirstOrDefaultAsync(x => x.Id == id);
        }
    }

    public async ValueTask<List<Review>> FilterByProductIdAsync(int productId)
    {
        using (context)
        {
            return await context.Reviews
                .Where(x => x.ProductId == productId)
                .OrderByDescending(x => x.CreateDate)
                .ToListAsync();
        }
    }

    public async ValueTask<int> PostAsync(Review review, Guid userId)
    {
        using (context)
        {
            var now = DateTime.Now;
            review.CreateDate = now;
            review.UpdateDate = now;
            review.UserId = userId;

            await context.Reviews.AddAsync(review);
            await context.SaveChangesAsync();
            return review.Id;
        }
    }

    public async ValueTask<int> PutAsync(Review review, Guid userId)
    {
        using (context)
        {
            var dbReview = await context.Reviews.FirstOrDefaultAsync(x => x.Id == review.Id);
            if (dbReview is null)
                return StatusCodes.Status404NotFound;

            if (dbReview.UserId != userId)
                return StatusCodes.Status400BadRequest;

            dbReview.Rating = review.Rating;
            dbReview.Title = review.Title;
            dbReview.ReviewText = review.ReviewText;
            dbReview.UpdateDate = DateTime.Now;
            await context.SaveChangesAsync();

            return StatusCodes.Status204NoContent;
        }
    }

    public async ValueTask<int> DeleteAsync(int id, Guid userId)
    {
        using (context)
        {
            var dbReview = await context.Reviews.FirstOrDefaultAsync(x => x.Id == id);
            if(dbReview is null)
                return StatusCodes.Status404NotFound;

            if (dbReview.UserId != userId)
                return StatusCodes.Status400BadRequest;

            context.Reviews.Remove(dbReview);
            await context.SaveChangesAsync();

            return StatusCodes.Status204NoContent;
        }
    }
}

