using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using TvShowsApi.Data.Models;

namespace TvShowsApi.Data
{
    public class TvShowsContext : ITvShowsContext
    {
        private readonly ILogger _logger;
        private const string COLLECTION_NAME = "shows";
        private const int PAGE_SIZE = 10;
        
        private readonly IMongoDatabase _database;
        
        public TvShowsContext(IMongoClient client, ILogger<TvShowsContext> logger)
        {
            _logger = logger;
            _database = client.GetDatabase("tvshowsdb");
        }

        public async Task InsertAsync(List<TvShow> shows)
        {
            var collection = _database.GetCollection<TvShow>(COLLECTION_NAME);
            
            try
            {
                _logger.LogDebug($"Inserting '{shows.Count}' TV Shows in the database");
                await collection.InsertManyAsync(shows);
                _logger.LogDebug("TV Shows inserted in the database.");
            }
            catch (Exception e)
            {
                _logger.LogDebug(e, "Error inserting TV Shows in the database");
                throw;
            }
        }

        public async Task<List<TvShow>> GetTvShowsAsync()
        {
            try
            {
                _logger.LogDebug("Retrieving all TV Shows from the database.");
                var collection = _database.GetCollection<TvShow>(COLLECTION_NAME);
                var shows = await collection
                    .Find(FilterDefinition<TvShow>.Empty)
                    .ToListAsync();

                _logger.LogInformation($"Retrieved TV Shows. Count: {shows.Count}");
                return shows;
            }
            catch (TimeoutException e)
            {
                _logger.LogError("Exception connecting to the database", e);
                throw;
            }
            catch (Exception e)
            {
                _logger.LogError("Exception retrieving data from the database", e);
                throw;
            }
        }
        
        public async Task<List<TvShow>> GetTvShowsPaginatedAsync(int page)
        {
            try
            {
                _logger.LogDebug($"Retrieving TV Shows from page {page} from the database.");
                
                var collection = _database.GetCollection<TvShow>(COLLECTION_NAME);
                var shows = await collection
                    .Find(FilterDefinition<TvShow>.Empty)
                    .Skip((page - 1) * PAGE_SIZE)
                    .Limit(PAGE_SIZE)
                    .ToListAsync();

                _logger.LogInformation($"Retrieved TV Shows. Count: {shows.Count}. From page: {page}");
                
                return shows;
            }
            catch (TimeoutException e)
            {
                _logger.LogError("Exception connecting to the database", e);
                throw;
            }
            catch (Exception e)
            {
                _logger.LogError("Exception retrieving data from the database", e);
                throw;
            }
        }
    }
}
