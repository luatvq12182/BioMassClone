const showConfirm = (message: string, cb: Function) => {
    const isConfirm = window.confirm(message);

    if (isConfirm) {
        cb();
    }
};

export { showConfirm };
