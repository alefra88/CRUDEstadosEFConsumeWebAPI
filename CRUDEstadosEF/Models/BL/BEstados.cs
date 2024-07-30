
using CRUDEstadosEF.Models.Entities;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace CRUDEstadosEF.Models.BL
{
    public class BEstados
    {
      
        private string _urlWebAPI;

        public BEstados()
        {
            //En el método constructor se cargará la URL de la web api que se especifico en appsettings.json
            //para esto se utiliza un objeto ConfigurationBuilder
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            _urlWebAPI = builder.GetSection("urlWebAPI").Value;
        }
        public async Task<List<Estados>> Consultar()
        {
            var estados = new List<Estados>();
            try
            {
                //instanciamos el objeto HttpClient
                using (var client = new HttpClient())
                {
                    //Invocamos el método GetAsync del objeto HttpClient, el cual envia una solicitud GET
                    //al URI especificando como parámetro, como una operación asincrónica
                    var responseTask = await client.GetAsync(_urlWebAPI);
                    //Se obtiene el objeto HttpResponseMessage en la variable responseTask

                    //Validamos el valor de la propiedad IsSuccessStatusCode del HttpResponseMessage
                    //para verificar que la operación haya sido ejecutada con éxito
                    if (responseTask.IsSuccessStatusCode)
                    {
                        //invocamos el método ReadStringAsync del objeto HttpContent el cual serializa
                        //el contenido HTTP en una cadena como una operación asincrónica
                        var responseJson = await responseTask.Content.ReadAsStringAsync();
                        //deserializamos el objeto recibido,en este caso una lista
                        estados = JsonConvert.DeserializeObject<List<Estados>>(responseJson);
                    }
                    else
                    {
                        throw new Exception($"WebAPI. Respondio con error. {responseTask.StatusCode}");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"WebAPI respondió con error {ex.Message}");
            }
            return estados;
        }
        public async Task<Estados> Consultar(int? id)
        {
            var estado = new Estados();
            try
            {
                //instanciamos el objeto HttpClient
                using (var client = new HttpClient())
                {
                    //Invocamos el método GetAsync del objeto HttpClient, el cual envia una solicitud GET
                    //al URI especificando como parámetro, como una operación asincrónica
                    var responseTask = await client.GetAsync(_urlWebAPI + $"/{id}");
                    //Se obtiene el objeto HttpResponseMessage en la variable responseTask

                    //Validamos el valor de la propiedad IsSuccessStatusCode del HttpResponseMessage
                    //para verificar que la operación haya sido ejecutada con éxito
                    if (responseTask.IsSuccessStatusCode)
                    {
                        //invocamos el método ReadStringAsync del objeto HttpContent el cual serializa
                        //el contenido HTTP en una cadena como una operación asincrónica
                        var responseJson = await responseTask.Content.ReadAsStringAsync();
                        //deserializamos el objeto recibido,en este caso una lista
                        estado = JsonConvert.DeserializeObject<Estados>(responseJson);
                    }
                    else
                    {
                        throw new Exception($"WebAPI. Respondio con error. {responseTask.StatusCode}");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"WebAPI respondió con error {ex.Message}");
            }
            return estado;
        }
        public async Task<Estados> Agregar(Estados estado)
        {
            try
            {
                //instanciamos la clase HttpClient
                using (var client = new HttpClient())
                {
                    //creamos un objeto HttpContent instanciando la clase StringContent, la cual es una
                    //clase que proporciona contenido HTTP basado en un string. Este contenido se crea
                    // con el objeto Estado que se esta recibiendo
                    HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(estado), Encoding.UTF8);
                    //asignamos al tipo application/json a la propiedad ContentType del encabezado HttpContent
                    httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    //invocamos el método PostAsync del objeto HttpClient,el cual envia una solicitud POST al
                    //URI especificado en los parámetros, como una operación asincrónica, asimismo le envía el
                    //contenido (objeto estado) dentro del HttpContent}
                    var responseTask = await client.PostAsync(_urlWebAPI, httpContent);
                    //verificamos la operación
                    if (responseTask.IsSuccessStatusCode)
                    {
                        //invocamos el método ReadAsStringAsync del objeto HttpContent el cual serializa
                        //el contenido HTTP en una cadena como una operación asincrónica.
                        //La Web API regresa un objeto tipo Estado
                        var responseJson = await responseTask.Content.ReadAsStringAsync();
                        //deserializamos el objeto recibido
                        estado = JsonConvert.DeserializeObject<Estados>(responseJson);
                    }
                    else
                    {
                        throw new Exception($"WebAPI. Respondió con error. {responseTask.StatusCode}");
                    }

                }
            }
            catch (Exception ex)
            {
                throw new Exception($"WebAPI. Respondió con error. {ex.Message}");
            }
            return estado;
        }
        public async Task Actualizar(Estados estado)
        {
            try
            {
                //instanciamos la clase HttpClient
                using (var client = new HttpClient())
                {
                    //creamos un objeto HttpContent instanciando la clase StringContent, la cual es una
                    //clase que proporciona contenido HTTP basado en un string. Este contenido se crea
                    // con el objeto Estado que se esta recibiendo
                    HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(estado), Encoding.UTF8);
                    //asignamos al tipo application/json a la propiedad ContentType del encabezado HttpContent
                    httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    //invocamos el método PostAsync del objeto HttpClient,el cual envia una solicitud POST al
                    //URI especificado en los parámetros, como una operación asincrónica, asimismo le envía el
                    //contenido (objeto estado) dentro del HttpContent}
                    var responseTask = await client.PutAsync(_urlWebAPI + $"/{estado.Id}", httpContent);
                    //verificamos la operación
                    if (responseTask.IsSuccessStatusCode)
                    {
                        //invocamos el método ReadAsStringAsync del objeto HttpContent el cual serializa
                        //el contenido HTTP en una cadena como una operación asincrónica.
                        //La Web API regresa un objeto tipo Estado
                        var responseJson = await responseTask.Content.ReadAsStringAsync();
                        //deserializamos el objeto recibido
                        estado = JsonConvert.DeserializeObject<Estados>(responseJson);
                    }
                    else
                    {
                        throw new Exception($"WebAPI. Respondió con error. {responseTask.StatusCode}");
                    }

                }
            }
            catch (Exception ex)
            {
                throw new Exception($"WebAPI. Respondió con error. {ex.Message}");
            }

        }
        public async Task Eliminar(int id)
        {
            try
            {
                //instanciamos el objeto HttpClient
                using (var client = new HttpClient())
                {
                    //Invocamos el método DeleteAsync del objeto HttpClient, el cual envia una solicitud DELETE
                    var responseTask = await client.DeleteAsync(_urlWebAPI + $"/{id}");
                    //Verificamos si la operación no fue exitosa se levanta una excepción
                    if (!responseTask.IsSuccessStatusCode)
                    {
                        throw new Exception($"WebAPI. Respondió con error. {responseTask.StatusCode}");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"WebAPI. Respondio con el error. {ex.Message}");
            }
        }
    }
}
