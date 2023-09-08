//using CVU.CONDICA.Application.Services.Mapping;
//using CVU.CONDICA.Application.Services.Pagination;
//using CVU.CONDICA.Dto.Pagination;
//using CVU.CONDICA.Dto.Positions;
//using MediatR;

//namespace CVU.CONDICA.Application.Positions.Queries
//{
//    public class PositionListQuery : IRequest<PaginatedModel<PositionDto>>
//    {
//        public PositionListQuery(PositionListQueryDto queryModel)
//        {
//            PositionList = queryModel;
//        }

//        public PositionListQueryDto PositionList { get; set; }
//    }

//    public class PositionListQueryHandler : RequestHandler<PositionListQuery, PaginatedModel<PositionDto>>
//    {
//        private readonly IPaginationService _paginationService;

//        public PositionListQueryHandler(IServiceProvider serviceProvider, IPaginationService paginationService) : base(serviceProvider)
//        {
//            _paginationService = paginationService;
//        }

//        public async override Task<PaginatedModel<PositionDto>> Handle(PositionListQuery request, CancellationToken cancellationToken)
//        {
//            var query = AppDbContext.Positions.AsQueryable();

//            var paginatedModel = _paginationService.PaginatedResults(query, request.PositionList, Mapping.PositionProjection);

//            return paginatedModel;
//        }
//    }
//}
