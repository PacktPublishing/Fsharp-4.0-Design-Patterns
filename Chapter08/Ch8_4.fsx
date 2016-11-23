let eagerList = [
    printfn "Evaluating eagerList"
    yield "I"
    yield "am"
    yield "an"
    yield "eager"
    yield "list"
]
// Evaluating eagerList
// val eagerList : string list = ["I"; "am"; "an"; "eager"; "list"]

let delayedEagerList = Seq.delay(fun () -> ([
                                            printfn "Evaluating eagerList"
                                            yield "I"
                                            yield "am"
                                            yield "an"
                                            yield "eager"
                                            yield "list"
                                        ] |> Seq.ofList))
// val delayedEagerList : seq<string>

delayedEagerList |> Seq.toList
// Evaluating eagerList
// val it : string list = ["I"; "am"; "an"; "eager"; "list"]

let src = [|1;2;3|]
let srcAsSeq = src :> seq<_>
let backdoor = srcAsSeq :?> int array
backdoor.[0] <- 10
printfn "%A" src

let srcAsROSeq = src |> Seq.readonly
let tryBackDoor = srcAsROSeq :?> int array // incur exception
// System.InvalidCastException: Unable to cast object of type 'mkSeq@541[System.Int32]' to type 'System.Int32[]'.
tryBackDoor.[0] <- 20
printfn "%A" src

let s = System.Collections.Stack()
s.Push(1)
s.Push('2')
s.Push("xyzzy")
s |> Seq.cast<_> |> printfn "%A" //!!!
