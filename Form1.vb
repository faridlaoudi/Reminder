Public Class Form1
    ' Using DateTime for accurate time comparison.
    Dim currentTime As DateTime
    Dim messageTime As DateTime
    Dim reminderShown As Boolean = False

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        ' Updating the current time display.
        UpdateCurrentTime()
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Initialize anything if needed, for example, setting up initial states of controls.
    End Sub

    Private Sub Timer3_Tick(sender As Object, e As EventArgs) Handles Timer3.Tick
        ' Checking if it's time to show the reminder.
        CheckReminder()
    End Sub

    Private Sub UpdateCurrentTime()
        ' Gets the current time and updates the label.
        currentTime = DateTime.Now
        Label1.Text = currentTime.ToString("hh:mm:ss tt")
    End Sub

    Private Sub CheckReminder()
        ' Checks if the current time is equal or has passed the message time and if the reminder hasn't been shown yet.
        If Not reminderShown AndAlso currentTime >= messageTime Then
            ShowReminder()
        End If
    End Sub

    Private Sub ShowReminder()
        ' Stops the timer, shows the message, and updates the UI accordingly.
        Timer3.Stop()
        MsgBox(TextBox1.Text)
        Button1.Enabled = True
        Button2.Enabled = False
        Label4.Text = ""
        reminderShown = True
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        ' Parses user input to set the reminder time and starts the timer.
        SetReminder()
        Timer3.Start()
        Button1.Enabled = False
        Button2.Enabled = True
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        ' Stops the reminder.
        Timer3.Stop()
        Button1.Enabled = True
        Button2.Enabled = False
        Label4.Text = ""
        reminderShown = False ' Allows the reminder to be set again.
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        ' Minimizes the application to the system tray and shows a balloon tip.
        MinimizeToTray()
    End Sub

    Private Sub NotifyIcon1_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles NotifyIcon1.MouseDoubleClick
        ' Restores the application from the system tray.
        RestoreFromTray()
    End Sub

    Private Sub SetReminder()
        ' Assuming MaskedTextBox1 contains the time in HH:MM format and ComboBox1 contains AM/PM.
        Dim parsedTime As DateTime
        If DateTime.TryParseExact(MaskedTextBox1.Text & " " & ComboBox1.Text, "hh:mm:ss tt", Nothing, Globalization.DateTimeStyles.None, parsedTime) Then
            ' Set the message time for today with the specified time.
            messageTime = DateTime.Today.Add(parsedTime.TimeOfDay)
            Label4.Text = "Reminder set for: " & messageTime.ToString("hh:mm:ss tt")
            reminderShown = False ' Ensures the reminder can trigger.
        Else
            MessageBox.Show("Invalid time format. Please enter the time as HH:MM.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Private Sub MinimizeToTray()
        NotifyIcon1.Visible = True
        Me.Hide()
        NotifyIcon1.ShowBalloonTip(3000)
    End Sub

    Private Sub RestoreFromTray()
        Me.Show()
        NotifyIcon1.Visible = False
    End Sub

End Class
