using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookApiProject.Models;

namespace BookApiProject.Services
{
    public class CountryRepository : ICountryRepository
    {
        private BookDbContext _countryContext;

        public CountryRepository(BookDbContext countryContext)
        {
            _countryContext = countryContext;
        }

        public bool CountryExists(int countryId)
        {
            return _countryContext.Country.Any(c => c.Id == countryId);
        }

        public async Task<bool> CreateCountry(Country country)
        {
            await _countryContext.AddAsync(country);
            return await Save();
        }

        public async Task<bool> DeleteCountry(Country country)
        {
            _countryContext.Remove(country);
            return await Save();
        }

        public ICollection<Author> GetAuthorsFromACountry(int countryId)
        {
            return _countryContext.Authors.Where(c => c.Country.Id == countryId).ToList();
        }

        public ICollection<Country> GetCountries()
        {
            return _countryContext.Country.OrderBy(c => c.Name).ToList();
        }

        public Country GetCountry(int countryId)
        {
            return _countryContext.Country.Where(c => c.Id == countryId).FirstOrDefault();
        }

        public Country GetCountryOfAnAuthor(int authorId)
        {
            return _countryContext.Authors.Where(a => a.Id == authorId).Select(c => c.Country).FirstOrDefault();
        }

        public bool IsDuplicateCountryName(int countryId, string countryName)
        {
            var country = _countryContext.Country.Where(c => c.Name.Trim().ToUpper() == countryName.Trim().ToUpper()
                                                && c.Id != countryId).FirstOrDefault();

            return country == null ? false : true;
        }

        public async Task<bool> Save()
        {
            var saved = await _countryContext.SaveChangesAsync();
            return saved >= 0 ? true : false;
        }

        public async Task<bool> UpdateCountryAsync(Country country)
        {
            _countryContext.Update(country);
            return await Save();
        }
    }
}
