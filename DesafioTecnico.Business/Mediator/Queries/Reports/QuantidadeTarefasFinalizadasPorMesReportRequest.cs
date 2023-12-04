using MediatR;

namespace DesafioTecnico.Business.Mediator.Queries.Reports
{
    public class QuantidadeTarefasFinalizadasPorMesReportRequest : IRequest<QuantidadeTarefasFinalizadasPorMesReportResponse>
    {
        public int Mes { get; set; } = DateTime.Now.Month;
        public int Ano { get; set; } = DateTime.Now.Year;
    }

    public class QuantidadeTarefasFinalizadasPorMesReportResponse
    {
        public IEnumerable<DataInfo> Data { get; set; } = Enumerable.Empty<DataInfo>();
        public int TotalRegistros => Data.Count();

        public class DataInfo
        {
            public int Mes { get; set; }
            public int Ano { get; set; }
            public string Usuario { get; set; }
            public int TotalTarefasConcluidas { get; set; }
        }
    }
}