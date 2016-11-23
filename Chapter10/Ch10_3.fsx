let inline nextHigher number =
    let g0 = LanguagePrimitives.GenericZero<'a>
    let g1 = LanguagePrimitives.GenericOne<'a>
    let g10 = (g1 <<< 3) + (g1 <<< 1)

    let toDigits n =
        let rec toDigitList digits n =
            if n = g0 then digits
            else toDigitList ((n % g10) :: digits) (n / g10)
        toDigitList [] n

    let fromDigits digits =
        let rec fromDigitList n = function
            | [] -> n
            | h::t -> fromDigitList (n * g10 + h) t
        fromDigitList g0 digits

    let make p ll  =
        ll |> List.rev |> List.partition ((<) p)
        |> fun (x,y) -> (x.Head::y) @ (p::(x.Tail))

    let rec scan (changing: 'a list) source =
        match source with
        | [] -> changing
        | h::t -> if h >= changing.Head then
                    scan (h::changing) t
                  else
                    (List.rev t) @ (make h changing)

    number |> toDigits |> List.rev
    |> fun x -> scan [(x.Head)] (x.Tail) |> fromDigits

nextHigher 1987654321
nextHigher 987654321L
nextHigher 32154321
nextHigher 12uy
nextHigher 5598734987954054911111111111111I
nextHigher 12222
nextHigher 136442n // It even works with nativeInts!!
//nextHigher 10.0 error: float does not support <<<
nextHigher 'A'
