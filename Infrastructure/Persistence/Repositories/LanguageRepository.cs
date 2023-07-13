using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repositories
{
    public class LanguageRepository : ILanguageRepository
    {
        private readonly IApplicationDbContext _dbContext;

        public LanguageRepository(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Language> GetByIdAsync(short languageId)
        {
            return await _dbContext.Languages.FindAsync(languageId);
        }

        public void Add(Language language)
        {
            _dbContext.Languages.Add(language);
        }

        public void Update(Language language)
        {
            _dbContext.Languages.Update(language);
        }

        public void Remove(Language language)
        {
            _dbContext.Languages.Remove(language);
        }
    }
}
