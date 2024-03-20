function DashboardClient({ client }) {
    return (
        <tr>
            <td>{client.name}</td>
            <td>{client.surname}</td>
            <td>{client.lastname}</td>
            <td>{client.currentCredit.toFixed(2)}</td>
            <td>{client.creditLimit.toFixed(2)}</td>
        </tr>
    );
}

export default DashboardClient;