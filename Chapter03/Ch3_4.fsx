let apply case arg =
    if case = 0 then
        sin arg
    elif case = 1 then
        cos arg
    elif case = 2 then
        asin arg
    elif case = 3 then
        acos arg
    else
        arg

let apply' case arg =
    try
        [|sin; cos; asin; acos|].[case] arg
    with
        | :?System.IndexOutOfRangeException -> arg

