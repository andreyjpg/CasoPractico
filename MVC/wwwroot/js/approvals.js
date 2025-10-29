$(document).ready(function () {
    const apiBase = "https://localhost:7169/api/Task";

    // Cargar todas las tareas
    function loadTasks() {
        $.ajax({
            url: `${apiBase}`,
            type: "GET",
            success: function (tasks) {
                const $body = $("#approvalsBody");
                $body.empty();

                tasks.sort((a, b) => {
                    const valA = a.approved === null ? 0 : a.approved ? 1 : 2;
                    const valB = b.approved === null ? 0 : b.approved ? 1 : 2;
                    return valA - valB;
                });

                tasks.forEach(task => {
                    const statusText = task.status || "Pending";
                    const approved = task.approved === null
                        ? "Pendiente"
                        : task.approved
                            ? "Aprobada"
                            : "Denegada";

                    const createdAt = task.createdAt
                        ? new Date(task.createdAt).toLocaleString()
                        : "-";

                    const row = `
                        <tr id="task-${task.id}">
                            <td>${task.id}</td>
                            <td>${task.name}</td>
                            <td>${task.description || ""}</td>
                            <td>${approved}</td>
                            <td>${createdAt}</td>
                            <td>
                                <button class="btn btn-success btn-sm approve-btn" data-id="${task.id}" ${task.approved === true ? "disabled" : ""}>Aprobar</button>
                                <button class="btn btn-danger btn-sm deny-btn" data-id="${task.id}" ${task.approved === false ? "disabled" : ""}>Denegar</button>
                            </td>
                        </tr>
                    `;
                    $body.append(row);
                });
            },
            error: function () {
                alert("Error al cargar las tareas.");
            }
        });
    }

    //  Accion: Aprobar
    $(document).on("click", ".approve-btn", function () {
        const id = $(this).data("id");
        $.ajax({
            url: `${apiBase}/Approve/${id}`,
            type: "PUT",
            success: function (msg) {
                alert(msg);
                loadTasks();
            },
            error: function (xhr) {
                alert(xhr.responseText || "Error al aprobar la tarea.");
            }
        });
    });

    //  Accion: Denegar
    $(document).on("click", ".deny-btn", function () {
        const id = $(this).data("id");
        $.ajax({
            url: `${apiBase}/Deny/${id}`,
            type: "PUT",
            success: function (msg) {
                alert(msg);
                loadTasks();
            },
            error: function (xhr) {
                alert(xhr.responseText || "Error al denegar la tarea.");
            }
        });
    });

    loadTasks();
});
