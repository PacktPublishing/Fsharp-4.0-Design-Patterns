let stopWatchGenerator (f:('a->'b)) (x: 'a) : (('a->'b)->'a->'b) =
    let whoRunsMe =
        System
            .Diagnostics
            .Process
            .GetCurrentProcess()
            .MainModule
            .FileName
        |> System.IO.Path.GetFileNameWithoutExtension
        |> sprintf "[%s]:" in
    fun f x ->
        let stopWatch = System.Diagnostics.Stopwatch() in
            try
                stopWatch.Start()
                f x
            finally
                printf "Took %dms in %s\n"
                    stopWatch.ElapsedMilliseconds
                    whoRunsMe

let whatItTakes f x = (stopWatchGenerator f x) f x

whatItTakes (fun x -> seq {1L .. x} |> Seq.sum) 10000000L

whatItTakes (fun cutoff ->
    (Seq.initInfinite (fun k -> (if k%2 = 0 then - 1.0 else 1.0)/((float k) * 2.0 - 1.0))
    |> Seq.skip 1
    |> Seq.take cutoff
    |> Seq.sum) * 4.0) 2000000
