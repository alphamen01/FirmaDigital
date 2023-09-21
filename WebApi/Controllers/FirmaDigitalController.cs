using AccesoDatos.Models;
using AccesoDatos.Operations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Controllers
{
    [Route("api")]
    [ApiController]
    public class FirmaDigitalController : ControllerBase
    {
        private readonly FirmadigitalDAO firmaDAO;
        private readonly IWebHostEnvironment env;

        public FirmaDigitalController(FirmadigitalDAO _firmaDAO, IWebHostEnvironment _env)
        {
            this.firmaDAO = _firmaDAO;
            this.env = _env;
        }

        [HttpGet("firmasDigitales")]
        public IActionResult GetFirmas()
        {
            var results = firmaDAO.seleccionarTodos();
            if (results != null)
            {
                return Ok(results);
            }
            else
            {
                return NotFound("No se encontraron registros de firmas.");
            }
        }

        [HttpGet("firmasDigitalesPaginado")]
        public IActionResult GetFirmasPaginado(int page, int size)
        {
            var results = firmaDAO.seleccionarTodosPaginado(page, size);
            if (results != null)
            {
                return Ok(results);
            }
            else
            {
                return NotFound("No se encontraron registros de firmas.");
            }
        }

        [HttpGet("firmaDigital/{id}")]
        public IActionResult GetFirma(int id)
        {
            var result = firmaDAO.seleccionar(id);
            if (result != null)
            {
                return Ok(result);
            }
            else {
                return NotFound("No se encontro registro de la firma.");
            }

        }

        [HttpPost("firmaDigital")]
        public IActionResult PostFirma([FromForm] Firmadigital firma, IFormFile file1, IFormFile file2)
        {

            if (file1 == null || file1.Length <= 0 || file2 == null || file2.Length <= 0)
            {
                return BadRequest("No se proporcionaron ambos archivos o al menos uno de ellos está vacío.");
            }

            var fileName1 = Path.GetFileName(file1.FileName);
            var fileName2 = Path.GetFileName(file2.FileName);

            var filePath1 = Path.Combine("uploads", fileName1);
            var filePath2 = Path.Combine("uploads", fileName2);

            using (var stream1 = new FileStream(filePath1, FileMode.Create))
            using (var stream2 = new FileStream(filePath2, FileMode.Create))
            {
                file1.CopyToAsync(stream1);
                file2.CopyToAsync(stream2);
            }

            var fileEntry = new Firmadigital
            {
                TipoFirma = firma.TipoFirma,
                RazonSocial = firma.RazonSocial,
                RepresentanteLegal = firma.RepresentanteLegal,
                EmpresaAcreditadora = firma.EmpresaAcreditadora,
                FechaEmision = firma.FechaEmision,
                FechaVencimiento = firma.FechaVencimiento,
                RutaRubrica = filePath1,
                CertificadoDigital = filePath2
            };

            firmaDAO.insertarFirma(fileEntry);
            return Ok("Firma digital insertada correctamente");

        }


        [HttpDelete("firmaDigital/{id}")]
        public IActionResult DeleteFirma(int id)
        {
            bool result = firmaDAO.eliminar(id);
            if (result == true)
            {
                return NoContent();
            }
            else
            {
                return NotFound();
            }

        }

        [HttpPost("firmaDigitalArchivo")]
        public IActionResult PostFirmaArchivos([FromForm] Firmadigital firma, IFormFile rubricaFile, IFormFile certificadoFile)
        {

            try
            {
                // Verifica si se proporcionaron archivos de rubrica y certificado
                if (rubricaFile == null || certificadoFile == null)
                {
                    return BadRequest("Se deben proporcionar archivos de rubrica y certificado.");
                }

                // Lee los datos binarios de los archivos
                byte[] rubricaData;
                byte[] certificadoData;

                using (var rubricaStream = new MemoryStream())
                using (var certificadoStream = new MemoryStream())
                {
                    rubricaFile.CopyToAsync(rubricaStream);
                    certificadoFile.CopyToAsync(certificadoStream);

                    rubricaData = rubricaStream.ToArray();
                    certificadoData = certificadoStream.ToArray();
                }



                // Asigna los datos binarios a las propiedades correspondientes
                //firma.RutaRubrica = Convert.ToBase64String(rubricaData);
                //firma.CertificadoDigital = Convert.ToBase64String(certificadoData);

                //// Inserta la entidad en la base de datos
                //_context.Firmadigitales.Add(firma);
                // _context.SaveChangesAsync();

                var fileEntry = new Firmadigital
                {
                    TipoFirma = firma.TipoFirma,
                    RazonSocial = firma.RazonSocial,
                    RepresentanteLegal = firma.RepresentanteLegal,
                    EmpresaAcreditadora = firma.EmpresaAcreditadora,
                    FechaEmision = firma.FechaEmision,
                    FechaVencimiento = firma.FechaVencimiento,
                    RutaRubrica = Convert.ToBase64String(rubricaData),
                    CertificadoDigital = Convert.ToBase64String(certificadoData)
                };

                firmaDAO.insertarFirma(fileEntry);
                return Ok("Firma digital insertada correctamente");


                //return CreatedAtAction("GetFirma", new { id = firma.IdFirma }, firma);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }

        }

        [HttpPost("firmaDigitalA")]
        public IActionResult PostFirmaA([FromForm] Firmadigital firma, IFormFile rubricaFile, IFormFile certificadoFile)
        {
            try
            {
                // Verifica si se proporcionaron archivos de rubrica y certificado
                if (rubricaFile == null || certificadoFile == null)
                {
                    return BadRequest("Se deben proporcionar archivos de rubrica y certificado.");
                }

                // Lee los datos binarios de los archivos
                byte[] rubricaData;
                byte[] certificadoData;

                using (var rubricaStream = new MemoryStream())
                using (var certificadoStream = new MemoryStream())
                {
                    rubricaFile.CopyToAsync(rubricaStream);
                    certificadoFile.CopyToAsync(certificadoStream);

                    rubricaData = rubricaStream.ToArray();
                    certificadoData = certificadoStream.ToArray();
                }

                // Asigna los datos binarios a las propiedades correspondientes
                firma.RutaRubrica = Convert.ToBase64String(rubricaData);
                firma.CertificadoDigital = Convert.ToBase64String(certificadoData);
            

                // Inserta la entidad en la base de datos
                firmaDAO.insertarFirma(firma);

                return CreatedAtAction("GetFirma", new { id = firma.IdFirma }, firma);
            }

            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }


    }
}
