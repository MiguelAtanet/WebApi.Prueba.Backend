using System;
using System.Collections.Generic;
using System.Linq;

namespace WebApi.Prueba.Backend.Dtos
{
    public class AsteroideDto
    {
       public string Nombre { get; set; }
        public double Diametro { get; set; }
        public string Velocidad { get; set; }
        public string Fecha { get; set; }
        public string Planeta { get; set; }
    }

    public static class AsteroideDtoExtensions
    {
        public static AsteroideDto MapAsteroideDtoNasaDtoToAsteroideDto(this Info infoAsteroides)
        {
            var diametroMedio = (infoAsteroides.EstimatedDiameter.Kilometers.EstimatedDiameterMax -
                                 infoAsteroides.EstimatedDiameter.Kilometers.EstimatedDiameterMin) / 2;
            var datosPlaneta = infoAsteroides.CloseApproachData.FirstOrDefault();

            return new AsteroideDto()
            {
                Nombre = infoAsteroides?.Name,
                Diametro = diametroMedio,
                Velocidad = datosPlaneta?.RelativeVelocity?.KilometersPerHour,
                Fecha = datosPlaneta?.CloseApproachDate,
                Planeta = datosPlaneta?.OrbitingBody
            };
        }
    }
}
