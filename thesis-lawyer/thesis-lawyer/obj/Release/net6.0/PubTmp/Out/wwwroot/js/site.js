$(function () {
    function displayMessages(messages) {
        messages.forEach(function (msg) {
            var $message = $('<div class="message ' + msg.message_side + '"><div class="avatar"></div><div class="text_wrapper"><div class="text">' + msg.text + '</div></div></div>');
            $('.messages').append($message);
            setTimeout(function () {
                $message.addClass('appeared');
            }, 0);
        });
    }

    function loadMessages() {
        $.ajax({
            url: '/Chat/chatlawyer',
            type: 'GET',
            success: function (data) {
                displayMessages(data);
            },
            error: function () {
                console.log('Error loading messages.');
            }
        });
    }

    // Load messages when the page loads
    loadMessages();

    function getMessageText() {
        return $('.message_input').val().trim();
    }

    function sendMessage(text) {
        if (text === '') {
            return;
        }
        $('.message_input').val('');
        var $messages = $('.messages');
        var message = {
            text: text,
            message_side: 'right'
        };
        displayMessages([message]);

        $.ajax({
            url: '/Chat/SendMessageToWatson/',
            type: 'POST',
            data: {
                message: text
            },
            success: function (response) {
                var answer = response.response;
                var answerMessage = {
                    text: answer,
                    message_side: 'left'
                };
                displayMessages([answerMessage]);
                $messages.animate({ scrollTop: $messages.prop('scrollHeight') }, 300);
            }
        });
    }

    $('.send_message').click(function () {
        sendMessage(getMessageText());
    });

    $('.message_input').keyup(function (e) {
        if (e.which === 13) {
            sendMessage(getMessageText());
        }
    });
});
