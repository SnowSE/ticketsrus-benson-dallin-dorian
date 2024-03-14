using Bunit;
using FluentAssertions;
using FluentAssertions.Execution;
using LibraryTRU.Data;
using LibraryTRU.Data.DTOs;
using LibraryTRU.Exceptions;
using LibraryTRU.IServices;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Engine.ClientProtocol;
using System.Net.Http;
using System.Net.Http.Json;



namespace TestsTRU;


public class RubricTests : IClassFixture<TRUWebAppFactory>
{
    HttpClient client;
    public RubricTests(TRUWebAppFactory factory)
    {
        client = factory.CreateDefaultClient();
    }

   
   

   



}