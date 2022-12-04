open InputUtils

let part1ScoreMap =
    Map.empty
    |> Map.add "A X" 4 // RR -> D + 1
    |> Map.add "A Y" 8 // RP -> W + 2
    |> Map.add "A Z" 3 // RS -> L + 3
    |> Map.add "B X" 1 // PR -> L + 1
    |> Map.add "B Y" 5 // PP -> D + 2
    |> Map.add "B Z" 9 // PS -> W + 3
    |> Map.add "C X" 7 // SR -> W + 1
    |> Map.add "C Y" 2 // SP -> L + 2
    |> Map.add "C Z" 6 // SS -> D + 3

let part2ScoreMap =
    Map.empty
    |> Map.add "A X" 3 // L(0) + S(3)
    |> Map.add "A Y" 4 // D(3) + R(1)
    |> Map.add "A Z" 8 // W(6) + P(2)
    |> Map.add "B X" 1 // L(0) + R(1)
    |> Map.add "B Y" 5 // D(3) + P(2)
    |> Map.add "B Z" 9 // W(6) + S(3)
    |> Map.add "C X" 2 // L(0) + P(2)
    |> Map.add "C Y" 6 // D(3) + S(3)
    |> Map.add "C Z" 7 // W(6) + R(1)

let part1 matches =
    matches
    |> Seq.map (fun str -> part1ScoreMap.[str])
    |> Seq.sum

let part2 matches =
    matches
    |> Seq.map (fun str -> part2ScoreMap.[str])
    |> Seq.sum

[<EntryPoint>]
let main argv =
    let input =
        System.IO.File.ReadLines (getFileName argv)
        |> Seq.cache

    part1 input
    |> printfn "%d"

    part2 input
    |> printfn "%d"

    0

