using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CommandQueries.Language.Queries.GetLanguages;
public record GetLanguagesQuery(short Id) : IRequest<LanguageResponse>;

public record LanguageResponse(
    short Id,
    string Name,
    string LanguageCode);