open Extensions
open InputUtils

let toString (chList: char list) = (System.String.Concat(Array.ofList(chList)))

let bruteForceFindOddOneOut (rucksack: char list): char =
    let middle = rucksack.Length / 2
    let firstHalf = rucksack |> List.take middle
    let secondHalf = rucksack |> List.skip middle

    firstHalf
    |> Seq.choose (fun needle -> List.tryFind ((=)needle) secondHalf)
    |> Seq.tryHead
    |> Option.orElseFail (sprintf "No odd one out found in '%s' & '%s'" (toString firstHalf) (toString secondHalf))

let findOddOneOut = bruteForceFindOddOneOut

let getPriority (ch: char): int =
    match System.Char.IsLower(ch), (int ch) with
    | true, ord -> ord - 96
    | false, ord -> ord - 64 + 26

let part1 rucksacks =
    rucksacks
    |> Seq.map findOddOneOut
    |> Seq.map getPriority
    |> Seq.sum

// ========== part 2 ==========

let findCommonElement (rucksacks: (char list)[]): char =
    if rucksacks.Length <> 3 then
        failwithf "expected 3 rucksacks, but got: %A" rucksacks

    rucksacks
    |> Seq.map Set.ofList
    |> Set.intersectMany
    |> Seq.tryExactlyOne
    |> Option.orElseFail (sprintf "No common element found in %A" rucksacks)

let part2 rucksacks =
    rucksacks
    |> Seq.chunkBySize 3
    |> Seq.map findCommonElement
    |> Seq.map getPriority
    |> Seq.sum

[<EntryPoint>]
let main argv =
    let input =
        System.IO.File.ReadLines (getFileName argv)
        |> Seq.map Seq.toList
        |> Seq.cache

    part1 input
    |> printfn "%d"

    part2 input
    |> printfn "%d"

    0

