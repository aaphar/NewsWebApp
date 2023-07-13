using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories;
public interface ILanguageRepository
{
    Task<Language> GetByIdAsync(short languageId);

    void Add(Language language);

    void Update(Language language);

    void Remove(Language language);
}

