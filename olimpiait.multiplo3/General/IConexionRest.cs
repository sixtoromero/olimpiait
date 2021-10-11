using olimpiait.multiplo3.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace olimpiait.multiplo3.General
{
    public interface IConexionRest
    {
        Task<HttpResponseWrapper<object>> Delete(string url);
        Task<HttpResponseWrapper<T>> Get<T>(string url);
        Task<HttpResponseWrapper<object>> Get(string url);
        Task<HttpResponseWrapper<object>> Post<T>(string url, T enviar);
        Task<HttpResponseWrapper<TResponse>> Post<T, TResponse>(string url, T enviar);        
        Task<HttpResponseWrapper<object>> PostFormData<TResponse>(string url, MultipartFormDataContent enviar);
        Task<HttpResponseWrapper<object>> Put<T>(string url, T enviar);
        Task<HttpResponseWrapper<TResponse>> Put<T, TResponse>(string url, T enviar);
    }
}
