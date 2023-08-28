import fetch from "node-fetch";

export async function handler(event, context) {
  try {
    const response = await fetch("https://jsonplaceholder.typicode.com/users");
    console.log("users.................");
    const data = await response.json();

    return {
      statusCode: 200,
      body: JSON.stringify(data),
    };
  } catch (error) {
    return {
      statusCode: 500,
      body: JSON.stringify({ error: "Internal Server Error" }),
    };
  }
}
