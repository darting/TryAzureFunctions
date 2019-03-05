namespace Startup

open Microsoft.Extensions.DependencyInjection
open Microsoft.Azure.WebJobs
open Microsoft.Azure.WebJobs.Hosting
open Giraffe
open Giraffe.Serialization.Json


type WebJobsExtensionStartup () =
    interface IWebJobsStartup with
        member __.Configure (builder : IWebJobsBuilder) =
            builder.AddBuiltInBindings() |> ignore
            builder.AddExecutionContextBinding() |> ignore
            builder.Services.AddGiraffe() |> ignore
            builder.Services.AddSingleton<IJsonSerializer>(Utf8JsonSerializer(Utf8JsonSerializer.DefaultResolver)) |> ignore

[<assembly: WebJobsStartup(typeof<WebJobsExtensionStartup>)>] do()
