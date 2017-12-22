module NumericsWSF.Interpolation

open ExcelDna.Integration
open ExcelDna.Documentation
open MathNet.Numerics.Interpolation

[<ExcelFunctionDoc(
    Name="NUM.INTERP.LINEAR",
    Category="Interpolation",
    Description="Linear interpolation",
    Remarks="Based on MathNet.Numerics.LinearSpline"
)>]
let linInterp 
        ([<ExcelArgument(Description="x values on which interpolation is based")>]
        x1:float[])

        ([<ExcelArgument(Description="y values on which interpolation is based")>]
        y1:float[]) 
        
        ([<ExcelArgument(Description="x values at which interpolated values will be calculated")>]
        x2:float[]) =

    let interp = LinearSpline.Interpolate(x1, y1)
    Array.map interp.Interpolate x2 |> Utils.packForCaller

[<ExcelFunctionDoc(
    Name="NUM.INTERP.CUBIC",
    Category="Interpolation",
    Description="Cubic natural interpolation",
    Remarks="Based on MathNet.Numerics.CubicSpline"
)>]
let cubicInterp
        ([<ExcelArgument(Description="x values on which interpolation is based")>]
        x1:float[])

        ([<ExcelArgument(Description="y values on which interpolation is based")>]
        y1:float[]) 
        
        ([<ExcelArgument(Description="x values at which interpolated values will be calculated")>]
        x2:float[]) =

    let interp = CubicSpline.InterpolateNatural(x1, y1)
    Array.map interp.Interpolate x2 |> Utils.packForCaller