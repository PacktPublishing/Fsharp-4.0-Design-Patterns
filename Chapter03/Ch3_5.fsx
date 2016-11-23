let simpleClosure =
    let scope = "old lexical scope"
    let enclose() =
        sprintf "%s" scope
    let scope = "new lexical scope"
    sprintf "[%s][%s]" scope (enclose())

let trackState seed =
    let state = ref seed in
    fun () -> incr state; (!state, seed)

let counter1 = trackState 5
counter1()
counter1()

let counter2 = trackState 100
counter2()
counter2()
