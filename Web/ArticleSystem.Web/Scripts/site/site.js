function fillReplyCommentForm(commentId) {
    var commentIdSelector = '#comment';

    var user = $(commentIdSelector + commentId + ' span.user-name').first().text();
    var date = $(commentIdSelector + commentId + ' span.comment-date').first().text();
    var comment = $(commentIdSelector + commentId + ' p.comment-text').first().text();

    $('#replyCommentUser').text(user);
    $('#replyCommentDate').text(date);
    $('#replyCommentText').text(comment);
}