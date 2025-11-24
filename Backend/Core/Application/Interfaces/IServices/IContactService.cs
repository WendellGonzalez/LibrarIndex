using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.DTOs;
using Domain.Entities;
using Microsoft.Extensions.Logging;

namespace Application.Interfaces.IServices
{
    public interface IContactService
    {
        Task ProcessContactInfo(ContactDTO contact);
    }
}