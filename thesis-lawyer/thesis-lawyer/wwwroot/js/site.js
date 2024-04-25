$(function () {
    function Message(arg) {
        this.text = arg.text;
        this.message_side = arg.message_side;
        this.draw = function () {
            var $message = $($('.message_template').clone().html());
            $message.addClass(this.message_side).find('.text').html(this.text);
            $('.messages').append($message);
            setTimeout(function () {
                $message.addClass('appeared');
            }, 0);
        };
        return this;
    }

    function getMessageText() {
        return $('.message_input').val().trim();
    }

    function sendMessage(text) {
        if (text === '') {
            return;
        }
        $('.message_input').val('');
        var $messages = $('.messages');
        var message = new Message({
            text: text,
            message_side: 'right'
        });
        message.draw();

        $.ajax({
            url: '/Chat/SendMessageToWatson/',
            type: 'POST',
            data: {
                message: text
            },
            success: function (response) {
                var answer = response.response;
                var answerMessage = new Message({
                    text: answer,
                    message_side: 'left'
                });
                answerMessage.draw();
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
