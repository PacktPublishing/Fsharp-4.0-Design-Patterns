#I __SOURCE_DIRECTORY__
#r @"../packages/FSharp.Data.SqlClient.1.8.1/lib/net40/FSharp.Data.SqlClient.dll"
open FSharp.Data
open System.Diagnostics

[<Literal>]
let connStr = @"Data Source=(localdb)\ProjectsV12;Initial Catalog=demo;Integrated Security=True"

type Mock = SqlCommandProvider<"exec MockQuery", connStr>

let querySync nReq =
    use cmd = new Mock()
    seq {
        for i in 1..nReq do
            yield (cmd.Execute() |> Seq.head)
        } |> Seq.sum

let query _ =
    use cmd = new Mock()
    async {
        let! resp = cmd.AsyncExecute()
        return (resp |> Seq.head)
    }

let queryAsync nReq =
    [| for i in 1..nReq -> i |]
    |> Array.map query
    |> Async.Parallel
    |> Async.RunSynchronously
    |> Array.sum

let timing header f args =
    let watch = Stopwatch.StartNew()
    f args |> printfn "%s %s %d" header "result ="
    let elapsed = watch.ElapsedMilliseconds
    watch.Stop()
    printfn "%s: %d %s %d %s" header elapsed "ms. for" args "requests"

timing "SyncIO" querySync 100
timing "AsyncIO" queryAsync 100