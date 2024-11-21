namespace AppAlquiler_WebAPI.Infrastructure.Dto
{
    public class ModelDetailsDto
    {
        public int? Id { get; set; }     //Agregado para front
        public string Name { get; set; }

        public bool Active { get; set; }

        //definicion de la marca
        public string Brand { get; set; }
    }
}
