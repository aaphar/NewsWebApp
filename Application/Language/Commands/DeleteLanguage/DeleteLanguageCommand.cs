using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Language.Commands.DeleteLanguage;
public record DeleteLanguageCommand(short Id) : IRequest;