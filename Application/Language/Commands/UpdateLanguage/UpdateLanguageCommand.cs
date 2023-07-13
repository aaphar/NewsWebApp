using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Language.Commands.UpdateLanguage;
public record UpdateLanguageCommand(
    short Id,
    string Name,
    string LanguageCode) : IRequest;

public record UpdateLanguageRequest(
    string Name,
    string LanguageCode) : IRequest;

