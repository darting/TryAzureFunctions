namespace Darting

open System
open System.Threading.Tasks
open Microsoft.AspNetCore.Mvc
open Microsoft.Azure.WebJobs
open Microsoft.Azure.WebJobs.Hosting
open Microsoft.AspNetCore.Http
open Microsoft.Azure.WebJobs.Host
open Microsoft.Azure.WebJobs.Extensions.Http
open Microsoft.Extensions.Logging
open Giraffe
open FSharp.Control.Tasks


module App =

    type ApiResult = 
        { Code : int
          Data : obj }

    let pong = "pong"
        // {| Code = 200; Data = "pong" |}

    let app : HttpHandler = 
        choose [
            GET >=> route "/api/healths/ping" >=> text pong
            GET >=> route "/api/healths/hello" >=> json { Code = 200; Data = "world" }
            GET >=> json "not found"
        ]

    [<FunctionName "Ping">]
    let run ([<HttpTrigger (AuthorizationLevel.Anonymous, Route = "{*any}")>] request : HttpRequest
            //  , context : ExecutionContext
             , log : ILogger) = 
        log.LogInformation "processed a request"
        let func = Some >> Task.FromResult
        task {
            let! _ = app func request.HttpContext
            do! request.HttpContext.Response.Body.FlushAsync()
        } :> Task
        
