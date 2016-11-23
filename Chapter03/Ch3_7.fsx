let s = "I'm a string"

let dict =
    System.Collections.Generic.Dictionary<string, string list>()

let gameOutcome isWin =
    "You " + if isWin then "win" else "loose"

// erroneous function definition - signature cannot be inferred
let truncator limit s =
    if s.Length > limit then
        s.Substring(0, limit)
    else
        s    
// explicit parameter definition
let truncator limit (s: string) =
    if s.Length > limit then
        s.Substring(0, limit)
    else
        s

let truncator' limit s =
    if not (System.String.IsNullOrEmpty s) && s.Length > limit then
        s.Substring(0, limit)
    else
        s

let logAndTrash ss =
    let log = System.Text.StringBuilder()
    for s in ss do
        sprintf "%A" s|> log.AppendLine |> ignore
    (ss :> System.IDisposable).Dispose()
    log
