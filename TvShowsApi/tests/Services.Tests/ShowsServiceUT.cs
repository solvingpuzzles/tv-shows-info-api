using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using TvShowsApi.Data;
using TvShowsApi.Data.Models;
using TvShowsApi.Services;
using Xunit;

namespace Services.Tests
{
    public class ShowsServiceUT
    {
        [Fact]
        public void GetTvShowsAsync_returns_empty_list_when_no_data_in_database()
        {
            int? page = null;
            var context = Substitute.For<ITvShowsContext>();
            var logger = Substitute.For<ILogger<ShowsService>>();
            context.GetTvShowsAsync().Returns(Task.FromResult(new List<TvShow>()));
            
            var sut = new ShowsService(context, logger);
            
            var result = sut.GetTvShowsAsync(page).GetAwaiter().GetResult();
            result.Should().BeEmpty();
        }
        
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-100)]
        public void GetTvShowsAsync_throws_ArgumentOutOfRangeException_exception_if_page_argument_lower_than_1(int? page)
        {
            var context = Substitute.For<ITvShowsContext>();
            var logger = Substitute.For<ILogger<ShowsService>>();

            var sut = new ShowsService(context, logger);
            
            Action result = () => sut.GetTvShowsAsync(page).GetAwaiter().GetResult();
            result.Should().Throw<ArgumentOutOfRangeException>();
        }

        [Fact]
        public void AddTvShowsAsync_empty_list_does_nothing()
        {
            var shows = new List<TvShow>();
            var context = Substitute.For<ITvShowsContext>();
            var logger = Substitute.For<ILogger<ShowsService>>();
            context.GetTvShowsAsync().Returns(Task.FromResult(new List<TvShow>()));
            
            var sut = new ShowsService(context, logger);

            sut.AddTvShowsAsync(shows).GetAwaiter().GetResult();

            context.DidNotReceive().InsertAsync(shows);
        }
        
        [Fact]
        public void AddTvShowsAsync_new_shows_inserts_in_database()
        {
            var fixture = new Fixture();
            var newShows = fixture.CreateMany<TvShow>(20).ToList();
            var existingShows = fixture.CreateMany<TvShow>(10).ToList();
            var context = Substitute.For<ITvShowsContext>();
            var logger = Substitute.For<ILogger<ShowsService>>();
            
            context.GetTvShowsAsync().Returns(Task.FromResult(existingShows));
            
            var sut = new ShowsService(context, logger);

            sut.AddTvShowsAsync(newShows).GetAwaiter().GetResult();

            context.Received().InsertAsync(newShows);
        }
        
        [Fact]
        public void AddTvShowsAsync_new_shows_inserts_in_database_except_duplicates()
        {
            var fixture = new Fixture();
            var existingShows = fixture.CreateMany<TvShow>(5).ToList();
            var newShows = fixture.CreateMany<TvShow>(5).ToList();
            var context = Substitute.For<ITvShowsContext>();
            var logger = Substitute.For<ILogger<ShowsService>>();
            
            // Trying to replicate duplicated TV Shows
            // The duplicates are removed when the 'AddTvShowsAsync' is called
            newShows.AddRange(existingShows);
            
            context.GetTvShowsAsync().Returns(Task.FromResult(existingShows));
            
            var sut = new ShowsService(context, logger);

            sut.AddTvShowsAsync(newShows).GetAwaiter().GetResult();
            
            context.Received().InsertAsync(newShows);
        }
    }
}
