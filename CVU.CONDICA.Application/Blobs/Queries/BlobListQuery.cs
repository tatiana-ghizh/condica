using CVU.CONDICA.Application.Services.Mapping;
using CVU.CONDICA.Dto.Blob;
using CVU.CONDICA.Dto.Enums;
using CVU.CONDICA.Persistence.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVU.CONDICA.Application.Blobs.Queries
{
    public class BlobListQuery : IRequest<IEnumerable<BlobDto>>
    {
        public BlobListQuery(int id, BlobComponent component, BlobQueryModel queryModel)
        {
            Id = id;
            Component = component;
            Types = queryModel?.Types;
        }

        public int Id { get; set; }
        public BlobComponent Component { get; set; }
        public BlobType[] Types { get; set; }
    }

    public class BlobListQueryHandler : RequestHandler<BlobListQuery, IEnumerable<BlobDto>>
    {

        public BlobListQueryHandler(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public async override Task<IEnumerable<BlobDto>> Handle(BlobListQuery request, CancellationToken cancellationToken)
        {

            IQueryable<Blob> query = Enumerable.Empty<Blob>().AsQueryable();

            switch (request.Component)
            {
                case BlobComponent.User:
                    query = AppDbContext.BlobUsers.Where(d => d.UserId == request.Id).Select(d => d.Blob).AsQueryable();
                    break;
            }

            if (request.Types != null && request.Types.Any())
            {
                query = query.Where(d => request.Types.Contains(d.BlobType));
            }

            var resultList = query
                .Select(Mapping.BlobProjection)
                .ToList();

            return resultList;
        }
    }
}
