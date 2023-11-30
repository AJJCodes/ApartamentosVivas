using Datos;
using Modelo.Cuartos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica.Cuartos
{
    public class Cuarto_LN
    {
        private readonly Contexto bd;

        public Cuarto_LN()
        {
            bd = new Contexto();
        }

        #region Poblar
        public bool ProporcionarListaCuartos(ref List<Cuarto_VM> ListaCuartos, out string errorMessage)
        {
            try
            {
                // Suponiendo que bd es tu objeto de contexto de base de datos
                var resultados = bd.spConseguirCuartos().ToList();

                // Mapear los resultados a la lista de UsuarioRoles_VM
                ListaCuartos = resultados.Select(c => new Cuarto_VM
                {
                    IdCuarto = c.IdCuarto,
                    DescripCuarto = c.DescripCuarto,
                    Costo = c.Costo , 
                    EstadoRenta = c.EstadoRenta,
                    EstadoMante = c.EstadoMante,
                    CodigoCuarto = c.CodigoCuarto,
                    CreadoPor = c.CreadoPor,
                    ModificaPor = c.ModificaPor
                }).ToList();

                errorMessage = null;  // No hay error, establecer el mensaje a null
                return true;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return false;
            }
        }

        public CuartoVista_VM PoblarVistaModificar(int Id)
        {
            try
            {
                var result = bd.spConseguirCuartoporId(Id).FirstOrDefault(); // Cambiado a FirstOrDefault para obtener un solo objeto

                if (result != null)
                {
                    var ResultadoConsulta = new CuartoVista_VM
                    {
                        IdCuarto = result.IdCuarto,
                        DescripCuarto = result.DescripCuarto,
                        Costo = result.Costo,
                        EstadoRenta = result.EstadoRenta,
                        EstadoMante = result.EstadoMante,
                        CodigoCuarto = result.CodigoCuarto
                    };

                    // Puedes realizar más operaciones si es necesario antes de devolver el resultado
                    return ResultadoConsulta;
                }
                else
                {
                    // Manejo del caso en que no se encuentra ningún objeto con el Id proporcionado
                    return null; // O lanzar una excepción u otro manejo según tu lógica
                }
            }
            catch (Exception ex)
            {
                // Manejo de la excepción
                Console.WriteLine($"Se produjo una excepción: {ex.Message}");

                throw; // O return null; o un valor predeterminado dependiendo de tu lógica
            }
        }


        #endregion

        #region Validaciones
        public bool VerificarExistenciaCodigo(string Codigo)
        {
            // Buscar el elemento en la tabla correspondiente
            var elemento = bd.Cuarto.FirstOrDefault(e => e.CodigoCuarto == Codigo);

            // Verificar si el elemento existe en la tabla
            if (elemento != null)
            {
                // El elemento ya existe en la tabla
                return true;
            }
            else
            {
                // El elemento no existe en la tabla
                return false;
            }
        }

        public bool ValidarCodigoConId(string Codigo, int ID)
        {
            // verificar si el codigo existe
            // el codigo pertenece a mi id?
            // si pertenece devolver true para permitir edicion
            // si no me pertenece false para no permitir

            if (!VerificarExistenciaCodigo(Codigo))
            {
                return true;
            }

            Cuarto e = bd.Cuarto.Find(ID);

            if (e != null && e.CodigoCuarto == Codigo)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        #endregion

        #region CRUD
        public bool AgregarCuarto(CuartoVista_VM Cuarto)
        {
            try
            {
                // Ejecuta el procedimiento almacenado
                bd.spAgregarCuarto(Cuarto.CodigoCuarto, Cuarto.DescripCuarto, Cuarto.EstadoRenta, Cuarto.EstadoMante, Cuarto.Costo,1);

                // Guarda los cambios en la base de datos
                bd.SaveChanges();

                // Si no se producen errores, devuelve true
                return true;
            }
            catch (Exception)
            {
                // Si ocurre un error, registra el mensaje de error (si es necesario) y devuelve false
                return false;
            }
        }

        public bool ModificarCuarto(CuartoVista_VM CuartoVM)
        {
            try
            {
                // Ejecuta el procedimiento almacenado generado automáticamente por Entity Framework
                bd.spModificarCuarto(CuartoVM.IdCuarto, CuartoVM.CodigoCuarto, CuartoVM.DescripCuarto, CuartoVM.Costo,1);//El 1 esta quemado temporalmente

                // Guarda los cambios en la base de datos
                bd.SaveChanges();

                // Si no se producen errores, devuelve true
                return true;
            }
            catch (Exception)
            {
                // Si ocurre un error, registra el mensaje de error (si es necesario) y devuelve false
                // Por ejemplo: _logger.LogError("Error al modificar empleado y contrato", ex);
                return false;
            }
        }

        //Eliminar El cuarto de Manera Forzada , No Logica
        public bool EliminarCuarto(int Id)
        {
            try
            {
                // Ejecuta el procedimiento almacenado generado automáticamente por Entity Framework
                bd.spEliminarCuarto(Id);

                // Guarda los cambios en la base de datos
                bd.SaveChanges();

                // Si no se producen errores, devuelve true
                return true;
            }
            catch (Exception)
            {
                // Si ocurre un error, registra el mensaje de error (si es necesario) y devuelve false
                // Por ejemplo: _logger.LogError("Error al modificar empleado y contrato", ex);
                return false;
            }
        }
        #endregion
    }
}
