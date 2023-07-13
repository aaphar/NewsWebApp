using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Language.Queries.GetLanguages;
public record GetLanguageQuery(short Id) : IRequest<LanguageResponse>;

public record LanguageResponse(
    short Id,
    string Name,
    string LanguageCode);