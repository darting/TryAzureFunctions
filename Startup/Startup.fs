namespace Startup

open Microsoft.Extensions.DependencyInjection
open Microsoft.Azure.WebJobs
open Microsoft.Azure.WebJobs.Hosting
open Giraffe
open Giraffe.Serialization.Json


type WebJobsExtensionStartup () =
    interface IWebJobsStartup with
        member __.Configure (builder : IWebJobsBuilder) =
            builder.Services.AddGiraffe()
                            .AddSingleton<IJsonSerializer>(Utf8JsonSerializer(Utf8JsonSerializer.DefaultResolver)) |> ignore

[<assembly: WebJobsStartup(typeof<WebJobsExtensionStartup>)>] do()
