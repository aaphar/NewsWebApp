using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CommandQueries.Language.Commands.UpdateLanguage;
public record UpdateLanguageCommand(
    short Id,
    string Name,
    string LanguageCode) : IRequest;

