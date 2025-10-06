using DigitalBankAssessment.Domain.Enum;

namespace DigitalBankAssessment.Domain.Entities
{
    public class Usuario
    {
        public int Id { get; set; }
        public required string Nombre { get; set; }
        public required DateTime FechaNacimiento { get; set; }
        public required string Sexo { get; set; }
        public required bool Activo {  get; set; }
    }
}
