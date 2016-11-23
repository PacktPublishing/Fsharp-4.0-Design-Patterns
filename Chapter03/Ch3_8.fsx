let cutter s =
    let cut s =
        printfn "imitator cut: %s" s
    let cut (s: string) =
        if s.Length > 0 then
            printfn "real cut: %s" s
            cut s.[1..]
        else
            printfn "finished cutting"
    cut s

let cutter s =
    let cut s =
        printfn "imitator cut: %s" s
    let rec cut (s: string) =
        if s.Length > 0 then
            printfn "real cut: %s" s
            cut s.[1..]
        else
            printfn "finished cutting"
    cut s
cutter "wow!"
