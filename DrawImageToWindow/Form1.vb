Imports System.Drawing.Drawing2D

Public Class Form1
    Private Declare Function GetAncestor Lib "user32.dll" (ByVal hwnd As Integer, ByVal GetAncestorFlags As Integer) As Integer
    Declare Function GetDesktopWindow Lib "user32" Alias "GetDesktopWindow" () As Integer
    Declare Function GetClientRect Lib "user32" Alias "GetClientRect" (ByVal hwnd As Integer, ByRef lpRect As RECT) As Integer
    Declare Function GetWindowRect Lib "user32" Alias "GetWindowRect" (ByVal hwnd As Integer, ByRef lpRect As RECT) As Integer
    Public Structure RECT
        Dim Left As Int32
        Dim Top As Int32
        Dim Right As Int32
        Dim Bottom As Int32
    End Structure

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        DrawImageToHandle(CInt(TextBox1.Text), Bitmap.FromFile("F:\开发资源\PNG\图标\Vista-desktop-12.png"), True)
    End Sub

    ''' <summary>
    ''' 把图像绘制到制定句柄指向的对象
    ''' </summary>
    ''' <param name="MyHandle">目标句柄</param>
    ''' <param name="MyImage">要绘制的图像</param>
    ''' <param name="Stretch">图像是否按句柄指向对象的尺寸拉伸绘制</param>
    ''' <remarks></remarks>
    Private Sub DrawImageToHandle(ByVal MyHandle As Integer, ByVal MyImage As Bitmap, Optional ByVal Stretch As Boolean = True)
        Dim MyRectangle As RECT
        Dim HandleOfParent As Integer = GetAncestor(MyHandle, 2)
        If HandleOfParent = 0 Then
            Call GetClientRect(MyHandle, MyRectangle)
            Debug.Print("顶级")
        Else
            Call GetWindowRect(MyHandle, MyRectangle)
            Debug.Print("子级")
        End If

        Dim MyGraphics As Graphics = Graphics.FromHwnd(MyHandle)
        MyGraphics.SmoothingMode = SmoothingMode.HighQuality
        MyGraphics.PixelOffsetMode = PixelOffsetMode.HighQuality

        If Not (Stretch) Then
            MyGraphics.DrawImage(MyImage, 0, 0, MyImage.Width, MyImage.Height)
        Else
            MyGraphics.DrawImage(MyImage, MyRectangle.Left, MyRectangle.Top, MyRectangle.Right - MyRectangle.Left, MyRectangle.Bottom - MyRectangle.Top)
        End If

        MyGraphics.Dispose()
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'TextBox1.Text = GetDesktopWindow
        TextBox1.Text = Me.Handle
        Debug.Print(GetAncestor(GetDesktopWindow, 3))
        Debug.Print(GetDesktopWindow)
    End Sub
End Class
