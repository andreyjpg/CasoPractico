$(function () {
    // --- add Roles ---
    const $addBtn = $('#addRoleBtn');
    const $form = $('#addRoleForm');
    const $saveBtn = $('#saveRoleBtn');
    const $cancelBtn = $('#cancelRoleBtn');

    $addBtn.on('click', function () {
        $form.removeClass('d-none');
        $addBtn.addClass('d-none');
    });

    $cancelBtn.on('click', function () {
        $form.addClass('d-none');
        $addBtn.removeClass('d-none');
    });
})