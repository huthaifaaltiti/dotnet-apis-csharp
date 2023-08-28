import fetch from "node-fetch";

export async function handler(event, context) {
  const { userNatNum } = event.queryStringParameters;

  try {
    const response = await fetch(
      `https://joyful-sunshine-d424f0.netlify.app/api/users?userNatNum=${userNatNum}`
    );
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
