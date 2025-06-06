Public Class Form1
Dim linecount As Integer &#39; the line position in the code
Dim RAMarray(100, 1) As String &#39; used to store variables the user declared
and also their values
Dim mainarray(100) As String &#39; used to store the code so it’s easier to handle
Dim command As String &#39; used to store the command that is currently being
processed
Dim Bdecide As Boolean &#39; decides how RAMarray is used
Dim VAR As String &#39;string thats on the left side of command
Dim VARVALUE As String &#39;string thats on the right side of command
Dim FOUNDlocation As Integer &#39;allows to modify rams contents
Dim RAMlocation As Integer &#39; position value within the ram
Dim position As Integer &#39;used in searhing the array
Dim accumulator As Integer &#39; stores the running value
Dim temp As String &#39; used to add subtract a value from accumulator
Dim loopcounter As Integer &#39; allows to search for a tag
Dim PLACE As Integer &#39; position within the text


Dim HASBEEN As Boolean &#39; boolean that tells the logic of the program if a
command has been detected
Dim TEMPWORD As String &#39; temporary word used to build strings individual
characters
Dim TEMPVAR As String &#39;holds data of the left side of the command
Dim wait1 As Integer &#39; delays the program by a certain amount
Dim executionpaths(100, 4) As String &#39;Array to store all necessary pointers
once the code is compiled
Dim ramcounter As Integer &#39; used to determine the allocation size of the
RAMarrray (depending on the amount of varibales that the user has declared
Dim notfound As Boolean &#39; boolaen that is passed so the program can verify
the existence of a decalared variable
Dim Num As Integer
Dim beenpressed As Integer
Dim start As Boolean
Dim stopcode As Boolean
Private Sub Button1_Click(sender As Object, e As EventArgs) Handles
Button1.Click &#39;Initializes the code
Call Initialazation()
End Sub
Sub Initialazation()
If start = False Then
Do Until linecount = (RichTextBox1.Lines.Length) &#39;a loop that reads
everything entered in the rich text box
mainarray(linecount) = RichTextBox1.Lines(linecount).ToString &#39; inserts
each line into the array
If linecount = 100 Then
MsgBox(&quot;code length exceeded&quot;)
Exit Sub
End If
linecount = linecount + 1
Loop

Me.Text = &quot;Big man computer (Running code....)&quot;
If linecount = 0 Then
MsgBox(&quot;Enter some code&quot;)
Call Reset()
Exit Sub
End If
linecount = linecount - 1
Bdecide = True
Do Until command = &quot;HLT&quot; Or command = &quot;COB&quot; &#39; this loop will repeat
unitl it finds &quot;HLT&quot; command
Call lexicalAnalisys()
If linecount = 0 Then
If command &lt;&gt; &quot;HLT&quot; Or command &lt;&gt; &quot;COB&quot; Then &#39; validation to
inform the user that they need to insert a &quot;HLT&quot; command
MsgBox(&quot;No HLT or COB command found&quot;)
Call Reset() &#39;precedure thet resets all variables to deafult state
Exit Sub
End If
ElseIf command = &quot;DAT&quot; Then &#39; if &quot;HLT&quot; inst reached then ramhandler
can populate the array with data
Call RamHandler()
If notfound = True Then
Call Reset() &#39;precedure thet resets all variables to deafult state
Exit Sub
End If
VAR = &quot;&quot;
End If
linecount = linecount - 1 &#39;decrements the linceount so variables can be
read from bottom to top
Loop
linecount = 0
Bdecide = False


command = &quot;&quot;
Do Until linecount = (RichTextBox1.Lines.Length) &#39;this loop looks for any
loops in the code
Call lexicalAnalisys()
If command = &quot;&quot; And Len(mainarray(linecount)) &lt;&gt; 0 And
Mid(mainarray(linecount), 1, 1) &lt;&gt; &quot;/&quot; Then &#39; verfiys whether correct syntax
been used.
MsgBox(&quot;Error on line: &quot; &amp; linecount + 1) &#39;Arrays are zero base so +1
allows to show &quot;one based&quot; loacation of the code
Call Reset()
Exit Sub
Else &#39;assign values to execution paths array on specific linecount value
executionpaths(linecount, 0) = command
executionpaths(linecount, 1) = VAR
executionpaths(linecount, 4) = VARVALUE
End If
If command = &quot;ADD&quot; Or command = &quot;SUB&quot; Or command = &quot;LDA&quot; Or
command = &quot;STA&quot; Or command = &quot;OUT&quot; Then
Call RamHandler()
If notfound = True Then
Call Reset() &#39;procedure thet resets all variables to deafult state
Exit Sub
End If
executionpaths(linecount, 2) = FOUNDlocation &#39;adds a direct adress
so the variable can be founfd in a array in O(1) time
End If
linecount = linecount + 1
command = &quot;&quot;
Loop
command = &quot;&quot;
linecount = 0


start = True &#39;validation to prevent the program crashing from start button
being pressed twice
Call Interpreter()
End If
End Sub
Sub lexicalAnalisys() &#39;used analyse the string on each line,pick up any
commands and integers.
PLACE = 1
If mainarray(linecount) &lt;&gt; &quot;&quot; Then &#39;this statment cchecks if the line is blank
Do Until PLACE = Len(mainarray(linecount)) + 1 &#39; this loop will iterate until
varable &quot;PLACE&quot; equal to the length of the array
TEMPWORD = Mid(mainarray(linecount), PLACE, 3) &#39;this variable holds
3 letters of the
If Asc(Mid(mainarray(linecount), PLACE, 1)) &lt;&gt; 32 And TEMPWORD &lt;&gt;
&quot;DAT&quot; And TEMPWORD &lt;&gt; &quot;LDA&quot; And TEMPWORD &lt;&gt; &quot;STA&quot; And TEMPWORD
&lt;&gt; &quot;SUB&quot; And TEMPWORD &lt;&gt; &quot;INP&quot; And TEMPWORD &lt;&gt; &quot;SUB&quot; And TEMPWORD
&lt;&gt; &quot;BRP&quot; And TEMPWORD &lt;&gt; &quot;BRZ&quot; And TEMPWORD &lt;&gt; &quot;OUT&quot; And
TEMPWORD &lt;&gt; &quot;HLT&quot; And TEMPWORD &lt;&gt; &quot;BRA&quot; And TEMPWORD &lt;&gt; &quot;COB&quot;
Then
If Mid(mainarray(linecount), PLACE, 1) = &quot;/&quot; Then &#39; if this symbbol is
present then the do loop stops since this symbol allows th user to comment
Exit Do
Else
TEMPVAR = TEMPVAR &amp; Mid(mainarray(linecount), PLACE, 1)
&#39;builds the string that is on th right side of a LMC command
End If
End If
If TEMPWORD = &quot;ADD&quot; Or TEMPWORD = &quot;SUB&quot; Or TEMPWORD =
&quot;BRZ&quot; Or TEMPWORD = &quot;STA&quot; Or TEMPWORD = &quot;BRP&quot; Or TEMPWORD = &quot;OUT&quot;
Or TEMPWORD = &quot;INP&quot; Or TEMPWORD = &quot;LDA&quot; Or TEMPWORD = &quot;DAT&quot; Or
TEMPWORD = &quot;HLT&quot; Or TEMPWORD = &quot;BRA&quot; Or TEMPWORD = &quot;COB&quot; Then


VAR = TEMPVAR &#39;if the command is found then left side of the
command is saved so it can be used to represent a loop tag or a variable
If VAR = &quot;&quot; Then &#39; this simple validation prevents type mismatches
VAR = 0
End If
TEMPVAR = &quot;&quot;
PLACE = PLACE + 2 &#39; place is incremented by 2 because the command
is a 3 letter string therefore by moving 2 places will allow to shift by one place
HASBEEN = True &#39; this boolean tells the program if the command was
found so the right side of the command can be found
command = TEMPWORD &#39; this passes the contents of TEMPword into
command variable so the other procedure knows which command was read
End If
If HASBEEN = True Then
VARVALUE = TEMPVAR &#39;this stores the right side of the command
End If
PLACE = PLACE + 1 &#39; position in the string is increased by 1 each
iteration
Loop
ElseIf mainarray(linecount) = &quot;&quot; Then
VAR = &quot;&quot;
VARVALUE = &quot;&quot;
command = &quot;&quot;
End If
HASBEEN = False &#39;some variables are set to deafult so the prcedure is ready
to be fed with new data
TEMPWORD = &quot;&quot;
TEMPVAR = &quot;&quot;
End Sub
Sub RamHandler() &#39; handles the RAMarray,used to update the ram&#39;s contents
If Bdecide = False Then &#39;this part is used for searching for data in the ram


Do Until VARVALUE = RAMarray(RAMlocation - position, 0) &#39;this part
compares the thing its looking for to the rams content in that loacation
position = position + 1
If position &gt; RAMlocation Then &#39;validation so the array doesnt get
searched in -1 position which is invalid
MsgBox(&quot;Could not locate variable at line : &quot; &amp; linecount + 1)
notfound = True
Exit Sub
End If
Loop
FOUNDlocation = (RAMlocation - position)
position = 0
ElseIf Bdecide = True Then &#39; this part is used for inserting new data into the
ram
If IsNumeric(VARVALUE) Then
RAMarray(RAMlocation, 1) = VARVALUE &#39;inserts the variable
Else
RAMarray(RAMlocation, 1) = 0 &#39;validation for assigning variables
End If
If RAMlocation = 100 Then &#39;limited ram space to 100 slots
MsgBox(&quot;RAM capacity exceeded!!!&quot;)
Call Reset()
notfound = True &#39;this boolean can be also used to exit the program if
ram capacity is ecxeeded
Else
RAMarray(RAMlocation, 0) = VAR &#39; inserts the variables value
RAMlocation = RAMlocation + 1
End If
End If
End Sub
Sub Interpreter() &#39; the part which performs a spsecific operation given the
command.

Dim temp As String &#39; used to add subtract a value from accumulator
Dim loopcounter As Integer &#39; allows to search for a tag
Dim accumulator As Integer &#39; stores the running value
Do While stopcode = False &#39; runs loop while the user has not pressed the
stop button is found
command = executionpaths(linecount, 0)
If wait1 &lt;&gt; 0 Then
TextBox1.Text = accumulator
Call graphics()
End If
If command = &quot;ADD&quot; Then &#39;adds a value to accumulator
If IsNumeric(executionpaths(linecount, 4)) = True Then &#39; checks if a
avriable is added or a integer
temp = RAMarray(executionpaths(linecount, 1), 1)
Else
temp = RAMarray(executionpaths(linecount, 2), 1)
End If
If accumulator &gt; 99999999 Or temp &gt; 99999999 Then
MsgBox(&quot;Overflow Error&quot;)
Call Reset()
Exit Sub
End If
accumulator = accumulator + temp &#39; adds the value to the acumulator
ElseIf command = &quot;SUB&quot; Then &#39;subtracts a value from the accumulator
If IsNumeric(executionpaths(linecount, 4)) = True Then &#39; checks if a
avriable is added or a integer
temp = RAMarray(executionpaths(linecount, 1), 1) &#39; allows to value
to be subtracted from accumulator
Else
temp = RAMarray(executionpaths(linecount, 2), 1)
End If
If accumulator &lt; -99999999 Or temp &gt; 99999999 Then

MsgBox(&quot;Overflow Error&quot;)
Call Reset()
Exit Sub
End If
accumulator = accumulator - temp &#39; subtracts the value to the
acumulator
ElseIf command = &quot;LDA&quot; Then &#39;loads a value into the accumulator
accumulator = RAMarray(executionpaths(linecount, 2), 1) &#39; sets
accumulator to the value that was loaded
ElseIf command = &quot;INP&quot; Then &#39; allows the user to input a valaue
temp = InputBox(&quot;INPUT&quot;)
If IsNumeric(temp) = False Or Len(temp) &gt; 8 Then &#39; validation to let only
integers to be entered
MsgBox(&quot;Invalid input,number is too big or type error&quot;)
Call Reset()
Exit Sub
Else
accumulator = temp &#39;accumulator set to the input
End If
ElseIf command = &quot;BRZ&quot; Then &#39;branches if a accumulator is zero
If accumulator = 0 Then
If executionpaths(linecount, 3) = &quot;&quot; Then
Do Until executionpaths(loopcounter, 1) =
executionpaths(linecount, 4) &#39;finds the location of the tag
loopcounter = loopcounter + 1
If loopcounter = RichTextBox1.Lines.Length Then &#39; determines if
the tag exists
MsgBox(&quot;Could not find the tag: &quot; &amp; executionpaths(linecount,
4))
Call Reset()
Exit Sub
End If

Loop
executionpaths(linecount, 3) = loopcounter - 1
linecount = executionpaths(linecount, 3)
loopcounter = 0
Else
linecount = executionpaths(linecount, 3)
End If
End If
ElseIf command = &quot;BRP&quot; Then &#39; branches if accumulator is positive
If accumulator &gt;= 0 Then
If executionpaths(linecount, 3) = &quot;&quot; Then
Do Until executionpaths(loopcounter, 1) =
executionpaths(linecount, 4) &#39;finds the location of the tag
loopcounter = loopcounter + 1
If loopcounter = RichTextBox1.Lines.Length Then &#39; determines if
the tag exists
MsgBox(&quot;Could not find the tag: &quot; &amp; executionpaths(linecount,
4))
Call Reset()
Exit Sub
End If
Loop
executionpaths(linecount, 3) = loopcounter - 1
linecount = executionpaths(linecount, 3)
loopcounter = 0
Else
linecount = executionpaths(linecount, 3)
End If
End If
ElseIf command = &quot;BRA&quot; Then &#39; branches unconditionally
If executionpaths(linecount, 3) = &quot;&quot; Then

Do Until executionpaths(loopcounter, 1) = executionpaths(linecount,
4) &#39;finds the location of the tag
loopcounter = loopcounter + 1
If loopcounter = RichTextBox1.Lines.Length Then &#39; determines if
the tag exists
MsgBox(&quot;Could not find the tag: &quot; &amp; executionpaths(linecount,
4))
Call Reset()
Exit Sub
End If
Loop
executionpaths(linecount, 3) = loopcounter - 1
linecount = executionpaths(linecount, 3)
loopcounter = 0
Else
linecount = executionpaths(linecount, 3)
End If
ElseIf command = &quot;STA&quot; Then &#39;stores the value of an accumulator into a
variabe
RAMarray(executionpaths(linecount, 2), 1) = accumulator
ElseIf command = &quot;OUT&quot; Then &#39;outputs the contents of the accumulator
If IsNumeric(executionpaths(linecount, 4)) = True Then
ListBox1.Items.Add(RAMarray(executionpaths(linecount, 4), 1))
ElseIf executionpaths(linecount, 4) = &quot;&quot; Then
ListBox1.Items.Add(accumulator)
Else
temp = RAMarray(executionpaths(linecount, 2), 1)
ListBox1.Items.Add(temp)
End If
ElseIf command = &quot;HLT&quot; Or command = &quot;COB&quot; Then
ListBox1.Items.Add(&quot;Execution complete.&quot;)
Call Reset()

Candidate Number: 4702 Centre Number: 57137
Exit Sub
End If
If beenpressed = True Then &#39;this if block allows the program to be
stepped through
Do While stopcode = False
If Num Mod 2 = 0 Then
Num = Num + 1
Exit Do
End If
wait(8)
Loop
End If
linecount = linecount + 1
Loop
Call Reset()
Exit Sub
End Sub
Sub graphics()
RichTextBox2.Clear()
Do Until ramcounter = RAMlocation
RichTextBox2.Text = RichTextBox2.Text &amp; (RAMarray(ramcounter, 0) &amp; &quot;
&quot; &amp; RAMarray(ramcounter, 1)) &amp; vbNewLine
ramcounter = ramcounter + 1
Loop
ramcounter = 0
If beenpressed = True Then
End If
wait(wait1)
RichTextBox1.Select(RichTextBox1.GetFirstCharIndexFromLine(linecount),
Len(mainarray(linecount)))
RichTextBox1.SelectionBackColor = Color.Blue
wait(wait1)

Candidate Number: 4702 Centre Number: 57137
RichTextBox1.Select(RichTextBox1.GetFirstCharIndexFromLine(linecount),
Len(mainarray(linecount)))
RichTextBox1.SelectionBackColor = Color.Black
wait(wait1)
RichTextBox2.Select(RichTextBox2.GetFirstCharIndexFromLine(executionpaths(li
necount, 2)), Len(RAMarray(executionpaths(linecount, 2), 0)))
RichTextBox2.SelectionBackColor = Color.Blue
wait(wait1)
RichTextBox2.Select(RichTextBox2.GetFirstCharIndexFromLine(executionpaths(li
necount, 2)), Len(RAMarray(executionpaths(linecount, 2), 0)))
RichTextBox2.SelectionBackColor = Color.Black
TextBox5.Text = command
TextBox3.Text = executionpaths(linecount, 2) + 1
TextBox4.Text = RAMarray(executionpaths(linecount, 2), 1)
TextBox2.Text = linecount + 1
End Sub
Sub Reset()
PLACE = 0
HASBEEN = False
TEMPWORD = &quot;&quot;
TEMPVAR = &quot;&quot;
linecount = 0
command = &quot;&quot;
VAR = &quot;&quot;
VARVALUE = 0
FOUNDlocation = 0
RAMlocation = 0
position = 0
PLACE = 0
HASBEEN = False
TEMPWORD = &quot;&quot;
TEMPVAR = &quot;&quot;

Candidate Number: 4702 Centre Number: 57137
accumulator = 0
temp = 0
loopcounter = 0
notfound = False
Array.Clear(mainarray, 0, mainarray.Length)
Array.Clear(RAMarray, 0, RAMarray.Length)
Array.Clear(executionpaths, 0, executionpaths.Length)
TextBox1.Text = &quot;&quot;
TextBox2.Text = &quot;&quot;
TextBox3.Text = &quot;&quot;
TextBox4.Text = &quot;&quot;
TextBox5.Text = &quot;&quot;
RichTextBox2.Clear()
wait1 = 30
Me.Text = &quot;Big Man Computer&quot;
Num = 0
beenpressed = False
start = False
stopcode = False
End Sub
Private Sub wait(ByVal interval As Integer)
Dim sw As New Stopwatch &#39; allows to delay the program
sw.Start()
Do While sw.ElapsedMilliseconds &lt; interval
&#39; Allows UI to remain responsive
Application.DoEvents()
Loop
sw.Stop()
End Sub
Private Sub LineSecondToolStripMenuItem_Click(sender As Object, e As
EventArgs) Handles LineSecondToolStripMenuItem.Click

Candidate Number: 4702 Centre Number: 57137
wait1 = 250
End Sub
Private Sub LinessecondToolStripMenuItem_Click(sender As Object, e As
EventArgs) Handles LinessecondToolStripMenuItem.Click
wait1 = 50
End Sub
Private Sub RealTimeToolStripMenuItem_Click(sender As Object, e As
EventArgs) Handles RealTimeToolStripMenuItem.Click
wait1 = 0
End Sub
Private Sub Button5_Click(sender As Object, e As EventArgs) Handles
Button5.Click
ListBox1.Items.Clear()
End Sub
Private Sub Button2_Click(sender As Object, e As EventArgs) Handles
Button2.Click
stopcode = True
RichTextBox1.SelectionBackColor = Color.Black
End Sub
Private Sub BLUEToolStripMenuItem_Click(sender As Object, e As EventArgs)
Handles BLUEToolStripMenuItem.Click
Me.BackColor = Color.LightBlue
End Sub
Private Sub REDToolStripMenuItem_Click(sender As Object, e As EventArgs)
Handles REDToolStripMenuItem.Click
Me.BackColor = Color.Red
End Sub
Private Sub GREENToolStripMenuItem_Click(sender As Object, e As
EventArgs) Handles GREENToolStripMenuItem.Click
Me.BackColor = Color.Green
End Sub

Candidate Number: 4702 Centre Number: 57137
Private Sub DEAFULTToolStripMenuItem_Click(sender As Object, e As
EventArgs) Handles DEAFULTToolStripMenuItem.Click
Me.BackColor = Color.DarkGray
End Sub
Private Sub Form1_Load(sender As Object, e As EventArgs) Handles
MyBase.Load
Me.Width = 479
wait1 = 50
End Sub
Private Sub Button3_Click(sender As Object, e As EventArgs) Handles
Button3.Click
If start = True Then
beenpressed = True
Num = Num + 1
Else
beenpressed = True
Num = Num + 1
Call Initialazation()
End If
wait1 = 250
End Sub
Private Sub ExtendedToolStripMenuItem_Click(sender As Object, e As
EventArgs) Handles ExtendedToolStripMenuItem.Click
Me.Width = 796
End Sub
Private Sub DeafultToolStripMenuItem1_Click(sender As Object, e As
EventArgs) Handles DeafultToolStripMenuItem1.Click
Me.Width = 479
End Sub
Private Sub ToolStripButton1_Click(sender As Object, e As EventArgs) Handles
ToolStripButton1.Click
Form2.Show()

End Sub
Private Sub FibSequenceToolStripMenuItem_Click(sender As Object, e As
EventArgs) Handles FibSequenceToolStripMenuItem.Click
Dim sb1 As New System.Text.StringBuilder
RichTextBox1.Clear()
sb1.AppendLine(&quot; INP / This a fibonacci sequence generator&quot;)
sb1.AppendLine(&quot; STA N&quot;)
sb1.AppendLine(&quot;loop LDA A&quot;)
sb1.AppendLine(&quot; SUB N&quot;)
sb1.AppendLine(&quot; BRP tag&quot;)
sb1.AppendLine(&quot; LDA A&quot;)
sb1.AppendLine(&quot; OUT&quot;)
sb1.AppendLine(&quot; LDA B&quot;)
sb1.AppendLine(&quot; ADD A&quot;)
sb1.AppendLine(&quot; STA ACC&quot;)
sb1.AppendLine(&quot; LDA B&quot;)
sb1.AppendLine(&quot; STA A&quot;)
sb1.AppendLine(&quot; LDA ACC&quot;)
sb1.AppendLine(&quot; STA B&quot;)
sb1.AppendLine(&quot; BRA loop&quot;)
sb1.AppendLine(&quot; tag HLT&quot;)
sb1.AppendLine(&quot;A DAT &quot;)
sb1.AppendLine(&quot;B DAT 1&quot;)
sb1.AppendLine(&quot;N DAT &quot;)
sb1.AppendLine(&quot;ACC DAT 0&quot;)
RichTextBox1.AppendText(sb1.ToString)
End Sub
Private Sub MultipilcationToolStripMenuItem_Click(sender As Object, e As
EventArgs) Handles MultipilcationToolStripMenuItem.Click
Dim sb2 As New System.Text.StringBuilder
RichTextBox1.Clear()

sb2.AppendLine(&quot; INP/will multiply two positive integers&quot;)
sb2.AppendLine(&quot; STA NUMA&quot;)
sb2.AppendLine(&quot; INP &quot;)
sb2.AppendLine(&quot; STA NUMB&quot;)
sb2.AppendLine(&quot;loop LDA TOTAL&quot;)
sb2.AppendLine(&quot; ADD NUMA&quot;)
sb2.AppendLine(&quot; STA TOTAL&quot;)
sb2.AppendLine(&quot; LDA NUMB&quot;)
sb2.AppendLine(&quot; SUB ONE&quot;)
sb2.AppendLine(&quot; STA NUMB&quot;)
sb2.AppendLine(&quot; BRP loop&quot;)
sb2.AppendLine(&quot; LDA TOTAL&quot;)
sb2.AppendLine(&quot; SUB NUMA&quot;)
sb2.AppendLine(&quot; STA TOTAL&quot;)
sb2.AppendLine(&quot; OUT&quot;)
sb2.AppendLine(&quot; HLT&quot;)
sb2.AppendLine(&quot;NUMA DAT&quot;)
sb2.AppendLine(&quot;NUMB DAT&quot;)
sb2.AppendLine(&quot;ONE DAT 1&quot;)
sb2.AppendLine(&quot;TOTAL DAT 0&quot;)
RichTextBox1.AppendText(sb2.ToString)
End Sub
Private Sub PrimeFinderexpertToolStripMenuItem_Click(sender As Object, e
As EventArgs) Handles PrimeFinderexpertToolStripMenuItem.Click
Dim sb3 As New System.Text.StringBuilder
RichTextBox1.Clear()
sb3.AppendLine(&quot; INP /enter a posisitve integer&quot;)
sb3.AppendLine(&quot; STA B&quot;)
sb3.AppendLine(&quot;loop LDA A&quot;)
sb3.AppendLine(&quot; OUT A&quot;)
sb3.AppendLine(&quot; ADD A&quot;)
sb3.AppendLine(&quot; STA A&quot;)

sb3.AppendLine(&quot; LDA B&quot;)
sb3.AppendLine(&quot; SUB A&quot;)
sb3.AppendLine(&quot; BRP loop&quot;)
sb3.AppendLine(&quot; COB&quot;)
sb3.AppendLine(&quot;A DAT 1&quot;)
sb3.AppendLine(&quot;B DAT&quot;)
RichTextBox1.AppendText(sb3.ToString)
End Sub
End Class