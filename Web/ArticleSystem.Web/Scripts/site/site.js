function fillReplyCommentForm(commentId) {
    var commentIdSelector = '#comment';

    var user = $(commentIdSelector + commentId + ' span.user-name').first().text();
    var date = $(commentIdSelector + commentId + ' span.comment-date').first().text();
    var comment = $(commentIdSelector + commentId + ' p.comment-text').first().text();

    $('#replyCommentUser').text('By ' + user);
    $('#replyCommentDate').text('(' + date + ')');
    $('#replyCommentText').text(comment);
    $('#replyToCommentWithId').val(commentId);

    $('#replyForm').show();
    $('#cover').show();
    $('#comment-form').hide();
}

function closeReplyCommentForm() {
    $('#replyForm').hide();
    $('#cover').hide();
    $('#comment-form').show();
}

// Close the Reply form modal when ESC key is hit
$(document).on('keydown', function (e) {
    var isModalVisible = $('#replyForm').is(":visible");

    if (e.keyCode === 27 && isModalVisible) {
        closeReplyCommentForm();
    }
});