$('span[data-action="up"').click(function () {
    var id = $(this).attr("data-id");
    voteClick(1, id);
});

$('span[data-action="down"').click(function () {
    var id = $(this).attr("data-id");
    voteClick(-1, id);
});

function voteClick(voteType, id) {
    $.post("/Votes/Vote", { articleId: id, voteType: voteType }, function (data) {
        var newVotesCount = data.Count;
        $('span[data-action="votesCount"][data-id="' + id + '"]').html(newVotesCount + ' votes');
    })
}