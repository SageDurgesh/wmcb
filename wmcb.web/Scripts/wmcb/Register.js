$(document).ready(function () {
    $("#btnRegister").click(function () {
        $.ajax({
            url: "SendEmail",
            data: { EmailAddress: "rani1984@gmail.com", Subject: "Test", Body: "Test" },
            method: "POST",
            dataType:"json"
        })   
        .done(function (){ 
                alert("done!");
            })        
    });
});