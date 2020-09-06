function banUser(username)
{
    var verificationToken = $("input[name='__RequestVerificationToken']").val ( ) //Znaci ovde ga dohvatimo, on se nalazi na svakoj starnici

    $.ajax ({  
        type: "POST", 
        url: "/User/BanUser", 
        data: { 
            "username": username, 
            "__RequestVerificationToken" : verificationToken 
        },
        dataType: "text",
        success: function ( response ) {
            $("#"+username).html(response)
        },
        error: function ( response ) {
            alert ( response ) 
        }
    })

}
function unbanUser(username)
{
    var verificationToken = $("input[name='__RequestVerificationToken']").val ( ) //Znaci ovde ga dohvatimo, on se nalazi na svakoj starnici

    $.ajax ({  
        type: "POST", 
        url: "/User/UnbanUser", 
        data: { 
            "username": username, 
            "__RequestVerificationToken" : verificationToken 
        },
        dataType: "text",
        success: function ( response ) {
            $("#"+username).html(response);
        },
        error: function ( response ) {
            alert ( "User with username: "+username+" does not exists!" ) ;
        }
    })

}